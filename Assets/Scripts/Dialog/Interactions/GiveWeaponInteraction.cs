using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New_GiveAttackInteraction", menuName ="New/DialogueInteraction/GiveWeaponInteraction")]
public class GiveWeaponInteraction : DialogueInteractionIE
{
    [SerializeField] Weapon_Scr weaponToGive;

    public override void DoSomething()
    {
        GiveWeapon();
    }

    public void GiveWeapon()
    {
        GameManager._GAME_MANAGER.player.ReceiveWeapon(weaponToGive);
    }

}
