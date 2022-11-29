using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{
    [SerializeField] int enemyLayer;
    int enemyLayerMask;

    public delegate void BattleStarted();
    public static  BattleStarted OnBattleStarted;

    private void Start()
    {
        int enemyLayerMask = (1 << enemyLayer);

        for (int i = 0; i < 10; i++)
        {
            ElementInventory.Add(GameManager._GAME_MANAGER._ELEMENT_FACTORY.CreateElement(Elements.FIRE));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == enemyLayer)
        {
            OnBattleStarted?.Invoke();
        }

    }

}
