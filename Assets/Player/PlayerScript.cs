using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    [SerializeField] int enemyLayer;
    int enemyLayerMask;

    public delegate void BattleStarted();
    public static  BattleStarted OnBattleStarted;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        enemyLayerMask = (1 << enemyLayer);

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == enemyLayer)
        {
              
            GameManager._GAME_MANAGER.LoadBattleScene();
            GameManager._GAME_MANAGER._BATTLE_MANAGER.player = this;
            GameManager._GAME_MANAGER._BATTLE_MANAGER.enemy= other.gameObject.GetComponent<EnemyScript>();
            this.gameObject.SetActive(false);

        }

    }

    public void InitializePosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
        
    }

}
