using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleStates { START_COMBAT, BEGIN_ROUND, PLAYER_ACTION, PLAYER_RESOLUTION,
    WIN, ENEMY_ACTION, ENEMY_RESOLUTION, LOSE, END_ROUND,END_COMBAT};
[System.Serializable]
public class BattleManager
{

    [SerializeField] public PlayerScript player;
    [SerializeField] public EnemyScript enemy;

    [SerializeField] public  TextMeshProUGUI playerHP;
    [SerializeField] public TextMeshProUGUI enemyHP;

    public SpellFactory _SPELL_FACTORY = new SpellFactory();

    static BattleInstance currentBattle;


    /*[SerializeField] public RelicUIManager relicUIManager;
    [SerializeField] public ElementUIManager elementUIManager;
    */
    public BattleUI_Manager Battle_UI;

    public delegate void ChangeStateEvent(BattleStates newStates);
    public static event ChangeStateEvent ChangeCombatState;


    public void PrepareBattle()
    {

        foreach (GameObject obj in GameManager._GAME_MANAGER._SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (obj.TryGetComponent<BattleUI_Manager>(out BattleUI_Manager man))
            {
                Battle_UI = man;

            }
        }
        currentBattle = new BattleInstance(this);

        ChangeCombatState += ChangeState;

        Battle_UI.SetSubMenus(player.EntityRelics, player.GetEntityStats.ElementInventory);

        GameManager._GAME_MANAGER.currentLevelInfo.gameObject.SetActive(true);
    }

    public void ChangeState(BattleStates newState)
    {

        //*DO SOME CHECKS FIRST*    
        currentBattle.currentState = newState;
    }

    public void SetRune(Relic rune)
    {
        currentBattle.SetRune(rune);
    }

    public void SetElements(List<ElementalBlock> elements)
    {
        currentBattle.SetElements(elements);
    }

   

    public void Update()
    {
        if (currentBattle == null) { return; }
        
        switch (currentBattle.currentState)
        {
            default:
            case BattleStates.START_COMBAT:
                currentBattle.StartCombat();
                break;
            case BattleStates.BEGIN_ROUND:
                Battle_UI.OnTurnBegin();
                currentBattle.BeginRound();
                break;
            case BattleStates.PLAYER_ACTION:
                currentBattle.PlayerAction();
                break;
            case BattleStates.PLAYER_RESOLUTION:
                currentBattle.PlayerResolution();
                break;
            case BattleStates.WIN:
                currentBattle.Win();
                break;
            case BattleStates.ENEMY_ACTION:
                currentBattle.EnemyAction();
                break;
            case BattleStates.ENEMY_RESOLUTION:
                currentBattle.EnemyResolution();
                break;
            case BattleStates.LOSE:
                currentBattle.Lose();
                break;
            case BattleStates.END_ROUND:
                currentBattle.EndRound();
                break;
            case BattleStates.END_COMBAT:
                currentBattle.EndCombat();
                break;
        }
    }


}