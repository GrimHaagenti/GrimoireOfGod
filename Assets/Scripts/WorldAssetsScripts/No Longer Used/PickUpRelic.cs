using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRelic : MonoBehaviour
{
    [SerializeField] Relic RelicToGivePlayer;


    private void OnTriggerEnter(Collider other)
    {
        if (Old_GameManager._GAME_MANAGER.playerScript._RelicInventory.Count < 3)
        {
            Old_GameManager._GAME_MANAGER.playerScript._RelicInventory.Add(RelicToGivePlayer);
            Destroy(gameObject);
        }
        else
        {
            //InputManager._INPUT_MANAGER.ChangeInputType(Scenes.BATTLE);
            Old_GameManager._GAME_MANAGER._UI_MANAGER.ChangeRelicPrompt(RelicToGivePlayer);
            Destroy(gameObject);
        }
    }
}
