using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStates { BEGIN_BATTLE, START_TURN, PLAYER_TURN, ENEMY_TURN, WIN, LOSE, END_BATTLE };
public class BattleSystem : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;

    BattleInstance currentBattle;

    public BattleStates currentState = BattleStates.PLAYER_TURN;


    public void StartBattle()
    {
        currentBattle = new BattleInstance(player,enemy, this);
    }
  
    void Start()
    {
        currentState = BattleStates.PLAYER_TURN;
        StartBattle();

    }

    void Update()
    {
        switch (currentState)
        {
            case BattleStates.BEGIN_BATTLE:
                currentBattle.OnBeginBattle();
                break;
            case BattleStates.START_TURN:
                break;
            case BattleStates.PLAYER_TURN:
                currentBattle.OnPlayerTurn();
                break;
            case BattleStates.ENEMY_TURN:
                break;
            case BattleStates.WIN:
                break;
            case BattleStates.LOSE:
                break;
            case BattleStates.END_BATTLE:
                break;
        }
    }

   
}