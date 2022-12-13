using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    [SerializeField] int enemyLayer;
    int enemyLayerMask;

    Vector3 latePos = Vector3.zero;

    [SerializeField] public Relic MainRelic;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == enemyLayer)
        {
              
            GameManager._GAME_MANAGER.LoadBattleScene(other.gameObject);
            GameManager._GAME_MANAGER._BATTLE_MANAGER.SetBattleEntities(this, other.gameObject);
            this.gameObject.SetActive(false);

        }

    }

    public void InitializePosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
        
    }

}
