using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_MaxHealthUpgrade", menuName = "New/DialogueInteraction/MaxHealthUpgrade")]
public class MaxHealthUpgrade : DialogueInteractionIE
{
    public override void DoSomething()
    {
        UpgradeMaxHealth();
    }
    public void UpgradeMaxHealth()
    {
        GameManager._GAME_MANAGER.player.GetMaxHPUpgrade();
    }


}