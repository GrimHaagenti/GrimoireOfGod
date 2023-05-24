using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSupportWeapon", menuName = "New/Weapons/SupportWeapon")]
public class Weapon_Support : Weapon_Scr
{
    [SerializeField] public SupportWeaponType_Enum supportWeaponType;
}
