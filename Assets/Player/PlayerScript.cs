using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    [SerializeField] int enemyLayer;
    int enemyLayerMask;

    Vector3 latePos = Vector3.zero;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        enemyLayerMask = (1 << enemyLayer);

    }


    private void Update()
    {
        if(latePos != gameObject.transform.position)
        {
            Debug.Log("player: " +GameManager._GAME_MANAGER._SCENE_MANAGER.currentScene.name);
            Debug.Log("player: " + gameObject.transform.position);

        }

        latePos = gameObject.transform.position;
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
