using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAdder : WorldInteractIE
{


    [SerializeField] ElementalBlock ElementToAdd;
    [SerializeField] int Quantity;
    public override void Interact()
    {
        for (int i = 0; i < Quantity; i++)
        {
            
            Old_GameManager._GAME_MANAGER.playerScript.AddToElementInventory(ElementToAdd);
            Old_GameManager._GAME_MANAGER.ShowMessage(Quantity + " " + ElementToAdd.BlockElement + " Elemental Blocks Added to Inventory!", 5f);
        }

    }
}
