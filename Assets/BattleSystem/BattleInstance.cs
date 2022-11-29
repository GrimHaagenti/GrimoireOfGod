using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleInstance
{


    
    BattleManager battleManager;
    
     RelicUIManager relicUIManager;
     ElementUIManager elementUIManager;

    public List<ElementalBlock> selectedBlocks { get; private set; }
    public Relic selectedRune { get; private set; }

    public BattleStates currentState = BattleStates.START_COMBAT;


    public BattleInstance( BattleManager battleManager)
    {
        this.battleManager = battleManager;
        
        GameObject.Instantiate(battleManager.enemy.EnemyPrefab, BattleScenarioManager._SCENERIO.enemyPosition.transform.position, Quaternion.identity, BattleScenarioManager._SCENERIO.enemyPosition.transform);

        GameManager._GAME_MANAGER.PlayerPrefab.transform.SetParent(BattleScenarioManager._SCENERIO.enemyPosition.transform);
        GameManager._GAME_MANAGER.PlayerPrefab.transform.position = Vector3.zero;

        GameManager._GAME_MANAGER.PlayerPrefab.SetActive(true);

        relicUIManager = battleManager.relicUIManager;
        elementUIManager = battleManager.elementUIManager;

        //BattleUI_Manager._UI_MANAGER.SetBattle(this.player, this.enemy);
        //BattleUI_Manager._UI_MANAGER.OnAtkButtonPressed.AddListener(PlayerAttack);

        currentState = BattleStates.START_COMBAT;
    }
    
    public void SetRune(Relic rune)
    {
        selectedRune = rune;
    }
    
    public void SetElements(List<ElementalBlock> elements)
    {
        selectedBlocks = elements;
    }
    public void StartCombat()
    {
        if(battleManager.player == null) { return; }
        if(battleManager.enemy == null) { return; }

        //battleManager.playerHP.text = battleManager.player.GetEntityStats.MaxHP.ToString();
        //battleManager.enemyHP.text = battleManager.enemy.GetEntityStats.MaxHP.ToString();

        relicUIManager?.SetSubmenu(battleManager.player.EntityRelics);
        elementUIManager?.SetSubmenu(battleManager.player.EntityElements);

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

        Debug.Log(selectedRune);
        Debug.Log(selectedBlocks.Count);
        selectedRune.Use(new List<Entity>() { battleManager.enemy }, selectedBlocks, battleManager.player);


        battleManager.playerHP.text = battleManager.player.GetEntityStats.MaxHP.ToString();
        battleManager.enemyHP.text = battleManager.enemy.GetEntityStats.MaxHP.ToString();



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

        battleManager.enemy.EntityRelics[Random.Range(0, battleManager.enemy.EntityRelics.Count - 1)].Use(new List<Entity>() { battleManager.player }, selectedBlocks, battleManager.enemy);


        battleManager.playerHP.text = battleManager.player.GetEntityStats.MaxHP.ToString();
        battleManager.enemyHP.text = battleManager.enemy.GetEntityStats.MaxHP.ToString();



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
