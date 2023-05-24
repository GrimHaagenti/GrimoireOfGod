using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Scr : ScriptableObject
{
    [SerializeField] protected string weapon_name;
    [SerializeField] protected int weapon_potency;
    [SerializeField] protected WeaponReach_Enum weaponReach;
    [SerializeField] protected List<Elements_Enum> ammoRequirement;
    [SerializeField] protected Elements_Enum elementAttack;
    [SerializeField] protected WeaponExtraFX_Enum weaponExtraFX;
    [SerializeField] protected GameObject vfx;
    [SerializeField] protected ItemType_Enum weaponType;

    #region
    public int Potency { get { return weapon_potency;} }
    public string WeaponName { get { return weapon_name;} }
    public ItemType_Enum Type { get { return weaponType; } }
    public Elements_Enum WeaponElement { get { return elementAttack; } }
    public WeaponReach_Enum Reach { get { return  weaponReach; } }
    public  List<Elements_Enum> Requirement { get { return ammoRequirement; } }
    public WeaponExtraFX_Enum ExtraEffect { get { return weaponExtraFX; } }
    public GameObject VisualEffect { get { return vfx; } }

    #endregion
}
