using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeWeapon", menuName = "New/Weapons/MeleeWeapon")]
public class Weapon_Melee : Weapon_Scr
{
    [SerializeField] protected MeleeWeaponType_Enum meleeWeaponType;
}
