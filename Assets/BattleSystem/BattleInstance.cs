using System.Collections.Generic;
using UnityEngine;


public class BattleInstance
{


    
    BattleManager battleManager;
  

    GameObject enemyObj;

    int MaxWeaknessCounter = 2;
    float damageMultiplier = 0.5f;
    float baseDamageMultiplier = 1f;

    public BattleStates currentState = BattleStates.START_COMBAT;

    Barrier PlayerBarrier = null;
    Barrier EnemyBarrier = null;

    public BattleInstance( BattleManager battleManager)
    {
        this.battleManager = battleManager;

        battleManager.enemy.InitEntity();

        enemyObj = GameObject.Instantiate(battleManager.enemy.EnemyPrefab, GameManager._GAME_MANAGER.currentLevelInfo.enemyPosition.transform.position, Quaternion.identity, GameManager._GAME_MANAGER.currentLevelInfo.enemyPosition.transform);

        enemyObj.transform.LookAt(GameManager._GAME_MANAGER.currentLevelInfo.CameraObject.transform);

        enemyObj.SetActive(true);

        GameManager._GAME_MANAGER.PlayerPrefab.SetActive(true);
        GameManager._GAME_MANAGER.PlayerPrefab.transform.LookAt(GameManager._GAME_MANAGER.currentLevelInfo.CameraObject.transform);


        //BattleUI_Manager._UI_MANAGER.SetBattle(this.player, this.enemy);
        //BattleUI_Manager._UI_MANAGER.OnAtkButtonPressed.AddListener(PlayerAttack);

        currentState = BattleStates.START_COMBAT;
    }

    public void SetRune(Relic rune)
    {
        battleManager.SetRune(rune);
    }

    
    public void StartCombat()
    {
        if(battleManager.player == null) { return; }
        if(battleManager.enemy == null) { return; }

        GameManager._GAME_MANAGER.currentLevelInfo.PlayerHealth.text = battleManager.player.CurrentHP + "/" + battleManager.player.GetEntityStats.MaxHP;
        GameManager._GAME_MANAGER.currentLevelInfo.EnemyHealth.text = battleManager.enemy.CurrentHP + "/" + battleManager.enemy.GetEntityStats.MaxHP;

       

        currentState = BattleStates.BEGIN_ROUND;
    }

    public void BeginRound()
    {
        //relicUIManager.gameObject.SetActive(true);
        //elementUIManager.gameObject.SetActive(false);
        currentState = BattleStates.PLAYER_ACTION;
    }

    public void PlayerAction()
    {

    }
    public void PlayerResolution()
    {
        //relicUIManager.gameObject.SetActive(false);
        //elementUIManager.gameObject.SetActive(false);

        
        TurnResolution res = battleManager.selectedRune.Use(new List<Entity>() { battleManager.enemy }, battleManager.player);

        if (res.UserBarrier != null)
        {
            PlayerBarrier = res.UserBarrier;
        }

        for (int i = 0; i < res.ApplyToUser.Count; i++)
        {
            battleManager.player.Heal(res.ApplyToUser[i]);
        }
        for (int i = 0; i < res.ApplyToTargets.Count; i++)
        {
            int damageAfterBarrier = res.ApplyToTargets[i];
            if(EnemyBarrier!= null)
            {
                damageAfterBarrier = EnemyBarrier.HitBarrier(damageAfterBarrier, res.ElementsUsed[i]);
            }
            int damageAfterWeakness = CheckWeaknesses(damageAfterBarrier, battleManager.enemy.GetEntityStats, res.ElementsUsed[i]);
            battleManager.enemy.GetHit(damageAfterWeakness);
        }

        GameManager._GAME_MANAGER.currentLevelInfo.PlayerHealth.text = battleManager.player.CurrentHP + "/" + battleManager.player.GetEntityStats.MaxHP;
        GameManager._GAME_MANAGER.currentLevelInfo.EnemyHealth.text = battleManager.enemy.CurrentHP + "/" + battleManager.enemy.GetEntityStats.MaxHP;


        currentState = BattleStates.WIN;

        


    }

    private int CheckWeaknesses(int dmg, EntityStat enemyStats, Elements elem)
    {
        int WeaknessCounter = 0;

        for (int i = 0; i < enemyStats.Weaknesses.Length; i++)
        {

            if (enemyStats.Weaknesses[i].attribute == elem)
            {
                WeaknessCounter++;
            }

        }

        WeaknessCounter = Mathf.Min(WeaknessCounter, MaxWeaknessCounter);

        float multiplier = baseDamageMultiplier + (WeaknessCounter * damageMultiplier);
        multiplier = Mathf.Max(multiplier, 1);
        int newDamage = Mathf.CeilToInt(dmg * multiplier);

        return newDamage;


    }
    public void Win()
    {
        if (battleManager.enemy.CurrentHP > 0)
        {
            currentState = BattleStates.ENEMY_ACTION;

        }
        else
        {
            currentState = BattleStates.END_COMBAT;
        }

    }

    public void EnemyAction()
    {
        currentState = BattleStates.ENEMY_RESOLUTION;


    }

    public void EnemyResolution()
    {
        TurnResolution res = new TurnResolution();
        if (battleManager.enemy._RelicInventory.Count > 0)
        {
            Relic enemyAtk = battleManager.enemy._RelicInventory[Random.Range(0, battleManager.enemy._RelicInventory.Count - 1)];
            enemyAtk.AddToRelicElements(GameManager._GAME_MANAGER._ELEMENT_FACTORY.GameElement[0]);
            res = enemyAtk.Use(new List<Entity>() { battleManager.player }, battleManager.enemy);
        }


        if (res.UserBarrier != null)
        {
            PlayerBarrier = res.UserBarrier;
        }

        for (int i = 0; i < res.ApplyToUser.Count; i++)
        {
            battleManager.enemy.Heal(res.ApplyToUser[i]);
        }
        for (int i = 0; i < res.ApplyToTargets.Count; i++)
        {
            int damageAfterBarrier = res.ApplyToTargets[i];
            if (PlayerBarrier != null)
            {
                damageAfterBarrier = PlayerBarrier.HitBarrier(damageAfterBarrier, res.ElementsUsed[i]);
            }
            int damageAfterWeakness = CheckWeaknesses(damageAfterBarrier, battleManager.player.GetEntityStats, res.ElementsUsed[i]);
            battleManager.player.GetHit(damageAfterWeakness);

        }

        
        GameManager._GAME_MANAGER.currentLevelInfo.PlayerHealth.text = battleManager.player.CurrentHP + "/" + battleManager.player.GetEntityStats.MaxHP;
        GameManager._GAME_MANAGER.currentLevelInfo.EnemyHealth.text = battleManager.enemy.CurrentHP + "/" + battleManager.enemy.GetEntityStats.MaxHP;





        currentState = BattleStates.LOSE;

    }
    public void Lose()
    {
        if (battleManager.player.CurrentHP > 0)
        {
            currentState = BattleStates.END_ROUND;

        }
        else
        {
            currentState = BattleStates.END_COMBAT;

        }
    }

    public void EndRound()
    {
        currentState = BattleStates.BEGIN_ROUND;

    }

    public void EndCombat()
    {

    }


}
