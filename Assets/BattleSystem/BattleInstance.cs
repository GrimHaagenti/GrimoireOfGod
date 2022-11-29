using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleInstance
{


    PlayerScript player;
    Entity enemy;
    BattleManager battleManager;
    
     RelicUIManager relicUIManager;
     ElementUIManager elementUIManager;

    public List<ElementalBlock> selectedBlocks { get; private set; }
    public Relic selectedRune { get; private set; }

    public BattleStates currentState = BattleStates.START_COMBAT;


    public BattleInstance(GameObject player, GameObject enemy, BattleManager battleManager)
    {
        this.player = player.GetComponent<PlayerScript>();
        this.enemy = enemy.GetComponent<Entity>();
        this.battleManager = battleManager;

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
        if(player == null) { return; }
        if(enemy == null) { return; }

        battleManager.playerHP.text = player.GetEntityStats.MaxHP.ToString();
        battleManager.enemyHP.text = enemy.GetEntityStats.MaxHP.ToString();

        relicUIManager?.SetSubmenu(player.EntityRelics);
        elementUIManager?.SetSubmenu(player.EntityElements);

        currentState = BattleStates.BEGIN_ROUND;
    }

    public void BeginRound()
    {
        relicUIManager.gameObject.SetActive(true);
        elementUIManager.gameObject.SetActive(false);
        currentState = BattleStates.PLAYER_ACTION;
    }

    public void PlayerAction()
    {

    }
    public void PlayerResolution()
    {
        relicUIManager.gameObject.SetActive(false);
        elementUIManager.gameObject.SetActive(false);

        Debug.Log(selectedRune);
        Debug.Log(selectedBlocks.Count);
        selectedRune.Use(new List<Entity>() { enemy }, selectedBlocks, player);


        battleManager.playerHP.text = player.GetEntityStats.MaxHP.ToString();
        battleManager.enemyHP.text = enemy.GetEntityStats.MaxHP.ToString();



        currentState = BattleStates.WIN;

        


    }


    public void Win()
    {
        if (enemy.GetEntityStats.MaxHP > 0)
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
        
        enemy.EntityRelics[Random.Range(0, enemy.EntityRelics.Count - 1)].Use(new List<Entity>() { player }, selectedBlocks, enemy);


        battleManager.playerHP.text = player.GetEntityStats.MaxHP.ToString();
        battleManager.enemyHP.text = enemy.GetEntityStats.MaxHP.ToString();



        currentState = BattleStates.LOSE;

    }
    public void Lose()
    {
        if (player.GetEntityStats.MaxHP > 0)
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
