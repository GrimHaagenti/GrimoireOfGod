using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New_MaxElementalUpgrade", menuName ="New/DialogueInteraction/MaxElementalUpgrade")]
public class MaxElemUpgrade : DialogueInteractionIE
{
    [SerializeField] private Elements_Enum elementToUpgrade;
    public override void DoSomething()
    {
        UpgradeMaxElem();
    }
    public void UpgradeMaxElem()
    {
        GameManager._GAME_MANAGER.player.GetElementalContainerUpgrade(elementToUpgrade);    
    }


}
