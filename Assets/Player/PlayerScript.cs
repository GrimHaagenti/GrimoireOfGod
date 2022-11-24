using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : Entity
{


    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            ElementInventory.Add(GameManager._GAME_MANAGER._ELEMENT_FACTORY.CreateElement(Elements.FIRE));
        }
    }


}
