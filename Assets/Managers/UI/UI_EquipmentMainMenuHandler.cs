using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EquipmentMainMenuHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI meleeWeaponText;
    [SerializeField] TextMeshProUGUI skill1WeaponText;
    [SerializeField] TextMeshProUGUI skill2WeaponText;
    [SerializeField] TextMeshProUGUI supportWeaponText;


    public void UpdatePlayerWeapons(New_Entity_Script player)
    {

        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.MeleeWeapon), meleeWeaponText);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.Skill1Weapon), skill1WeaponText);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.Skill2Weapon), skill2WeaponText);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.SupportWeapon), supportWeaponText);

    }
    private void SetWeaponSlotText(Weapon_Scr? weapon, TextMeshProUGUI slot)
    {
        if(weapon == null)
        {
            slot.text = "-----";
        }
        else
        {
            slot.text = weapon.WeaponName;
        }
    }
}