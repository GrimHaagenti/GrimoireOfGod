using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleInstance
{


    PlayerScript player;
    Entity enemy;
    BattleSystem battleManager;


    public BattleInstance(GameObject player, GameObject enemy, BattleSystem battleManager)
    {
        this.player = player.GetComponent<PlayerScript>();
        this.enemy = enemy.GetComponent<Entity>();
        this.battleManager = battleManager;

        UiManager._UI_MANAGER.SetBattle(this.player, this.enemy);
        UiManager._UI_MANAGER.OnAtkButtonPressed.AddListener(PlayerAttack);


    }
    private void Start()
    {

    }

    public void OnBeginBattle()
    {

    }

    public void OnStartTurn()
    {

    }
    public void OnPlayerTurn()
    {

    }

    public void OnEnemyTurn()
    {

    }

    public void OnWin()
    {

    }

    public void OnLose()
    {

    }
    public void OnEndBattle()
    {

    }

    void PlayerAttack()
    {
        enemy.GetHit(1);
        Debug.Log("AAA");
    }

    void EnemyAttack()
    {
        player.GetHit(1);
    }

}
