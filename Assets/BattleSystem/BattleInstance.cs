using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleInstance
{


    
    BattleManager battleManager;
  

    GameObject enemyObj;

    

    public BattleStates currentState = BattleStates.START_COMBAT;


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

    public void SetElements(List<ElementalBlock> elements)
    {
        battleManager.SetElements(elements);
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

        
        battleManager.selectedRune.Use(new List<Entity>() { battleManager.enemy }, battleManager.player);


        GameManager._GAME_MANAGER.currentLevelInfo.PlayerHealth.text = battleManager.player.CurrentHP + "/" + battleManager.player.GetEntityStats.MaxHP;
        GameManager._GAME_MANAGER.currentLevelInfo.EnemyHealth.text = battleManager.enemy.CurrentHP + "/" + battleManager.enemy.GetEntityStats.MaxHP;


        currentState = BattleStates.WIN;

        


    }


    public void Win()
    {
        if (battleManager.enemy.GetEntityStats.MaxHP > 0)
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

        battleManager.enemy.EntityRelics[Random.Range(0, battleManager.enemy.EntityRelics.Count - 1)].Use(new List<Entity>() { battleManager.player }, battleManager.enemy);


        GameManager._GAME_MANAGER.currentLevelInfo.PlayerHealth.text = battleManager.player.CurrentHP + "/" + battleManager.player.GetEntityStats.MaxHP;
        GameManager._GAME_MANAGER.currentLevelInfo.EnemyHealth.text = battleManager.enemy.CurrentHP + "/" + battleManager.enemy.GetEntityStats.MaxHP;


        currentState = BattleStates.LOSE;

    }
    public void Lose()
    {
        if (battleManager.player.GetEntityStats.MaxHP > 0)
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
