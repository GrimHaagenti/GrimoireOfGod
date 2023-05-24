using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WeaponListItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponName;

    [SerializeField] public Button UI_ItemButton;

    int weaponInventoryIndex = -1;
    int weaponItemIndex = -1;

    public void SetWeaponItemName(Weapon_Scr weapon, int inv_index)
    {
        weaponName.text = weapon.WeaponName;
        weaponItemIndex = ItemManager._ITEM_MANAGER.GetWeaponIndex(weapon);
        weaponInventoryIndex = inv_index;
    }

    public int GetWeaponItemIndex()
    {
        return weaponItemIndex;
    }
    public int GetWeaponInventoryIndex()
    {
        return weaponInventoryIndex;
    }
}
