using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Equipment
{
    private int meleeSlot = -1;
    private int skill1Slot = -1;
    private int skill2Slot = -1;
    private int supportSlot = -1;


    public Entity_Equipment (Equipment_Scr equip)
    {
        
        meleeSlot = equip.MeleeWeapon;
        skill1Slot = equip.Skill1Weapon;
        skill2Slot = equip.Skill2Weapon;
        supportSlot = equip.SupportWeapon;
        
    }
    public void SetWeapon(int weaponInventoryIndex, int WeaponSlot)
    {
        switch (WeaponSlot)
        {
            case 0:
                meleeSlot = weaponInventoryIndex;
                break;
            case 1:
                skill1Slot = weaponInventoryIndex;
                break;
            case 2:
                skill2Slot = weaponInventoryIndex;
                break;
            case 3:
                supportSlot = weaponInventoryIndex;
                break;
        }
    }
    public void ClearSlot(int WeaponSlot)
    {
        switch (WeaponSlot)
        {
            case 0:
                meleeSlot = -1;
                break;
            case 1:
                skill1Slot = -1;
                break;
            case 2:
                skill2Slot = -1;
                break;
            case 3:
                supportSlot = -1;
                break;
        }
    }

    public int MeleeWeapon { get { return meleeSlot; } }
    public int Skill1Weapon { get { return skill1Slot; } }
    public int Skill2Weapon { get { return skill2Slot; } }
    public int SupportWeapon { get { return supportSlot; } }
}
