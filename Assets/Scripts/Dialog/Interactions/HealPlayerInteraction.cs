using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "HealPlayer_IE", menuName ="New/DialogueInteraction/HealPlayerInteraction")]
public class HealPlayerInteraction : DialogueInteractionIE
{
    public override void DoSomething()
    {
        HealPlayer();
    }

    private void HealPlayer()
    {
        GameManager._GAME_MANAGER.player.ReplenishPlayer();
    }
}
