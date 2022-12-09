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

    public PlayerScript player;
    public EnemyScript enemy;
    private GameObject enemyObj;

    [SerializeField] public  TextMeshProUGUI playerHP;
    [SerializeField] public TextMeshProUGUI enemyHP;


    static BattleInstance currentBattle;

    public List<ElementalBlock> selectedBlocks { get; private set; }
    public Relic selectedRune { get; private set; }

    public UIManager Battle_UI;

    public delegate void ChangeStateEvent(BattleStates newStates);
    public static event ChangeStateEvent ChangeCombatState;

    public void SetBattleEntities(PlayerScript player, GameObject enemy)
    {
        this.player = player;
        enemyObj = enemy;
        this.enemy = enemyObj.GetComponent<EnemyScript>();
    }
    public void PrepareBattle()
    {
        selectedBlocks = new List<ElementalBlock>();
        selectedRune = null;
        Battle_UI = GameManager._GAME_MANAGER._UI_MANAGER;
        /*
        foreach (GameObject obj in GameManager._GAME_MANAGER._SCENE_MANAGER.currentScene.GetRootGameObjects())
        {
            if (obj.TryGetComponent<BattleUI_Manager>(out BattleUI_Manager man))
            {
                Battle_UI = man;

            }
        }
        */
        currentBattle = new BattleInstance(this);

        ChangeCombatState += ChangeState;

        //Battle_UI.SetSubMenus(player.EntityRelics, player.GetEntityStats.ElementInventory, this);

        GameManager._GAME_MANAGER.currentLevelInfo.gameObject.SetActive(true);
    }

    public void ChangeState(BattleStates newState)
    {

        //*DO SOME CHECKS FIRST*    
        currentBattle.currentState = newState;
    }

   

    public void SetRune(Relic rune)
    {
        selectedRune = rune;
    }
    public void SetElements(List<ElementalBlock> elements)
    {
        selectedBlocks = elements;
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
                currentBattle = null;
                GameObject.Destroy(enemyObj);
                player.gameObject.SetActive(false);
                GameManager._GAME_MANAGER.UnloadBattleScene();
                break;
        }
    }


}