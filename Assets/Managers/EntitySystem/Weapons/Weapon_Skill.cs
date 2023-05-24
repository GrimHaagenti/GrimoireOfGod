using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newSkillWeapon", menuName = "New/Weapons/SkillWeapon")]
public class Weapon_Skill : Weapon_Scr
{
    [SerializeField] protected SkillWeaponType_Enum skillWeaponType;
}
