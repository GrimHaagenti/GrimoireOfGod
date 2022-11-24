using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BattleStates { START_COMBAT, BEGIN_ROUND, PLAYER_ACTION, PLAYER_RESOLUTION,
    WIN, ENEMY_ACTION, ENEMY_RESOLUTION, LOSE, END_ROUND,END_COMBAT};
public class BattleManager : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

     [SerializeField] public  TextMeshProUGUI playerHP;
    [SerializeField] public TextMeshProUGUI enemyHP;

    public SpellFactory _SPELL_FACTORY = new SpellFactory();

    BattleInstance currentBattle;
    [SerializeField] public RelicUIManager relicUIManager;
    [SerializeField] public ElementUIManager elementUIManager;

    public delegate void ChangeStateEvent(BattleStates newStates);
    public static event ChangeStateEvent ChangeCombatState;

   

    public void ChangeState(BattleStates newState)
    {

        //*DO SOME CHECKS FIRST*    
        currentBattle.currentState = newState;
    }
    public void StartBattle()
    {
        ChangeCombatState += ChangeState;
       currentBattle = new BattleInstance(player,enemy, this);
    }

    public void SetRune(Rune rune)
    {
        currentBattle.SetRune(rune);
    }

    public void SetElements(List<ElementalBlock> elements)
    {
        currentBattle.SetElements(elements);
    }

    void Start()
    {
        StartBattle();
        currentBattle.currentState = BattleStates.START_COMBAT;

    }

    void Update()
    {

        
        switch (currentBattle.currentState)
        {
            default:
            case BattleStates.START_COMBAT:
                currentBattle.StartCombat();
                break;
            case BattleStates.BEGIN_ROUND:
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