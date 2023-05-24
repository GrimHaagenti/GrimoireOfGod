using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="entity_Equipment", menuName ="New/Entity/Equipment")]
public class Equipment_Scr: ScriptableObject
{

    // Inventory References
    [SerializeField] private int meleeSlot = -1;
    [SerializeField] private int skill1Slot = -1;
    [SerializeField] private int skill2Slot = -1;
    [SerializeField] private int supportSlot = -1;

    public int MeleeWeapon {get {return meleeSlot;} }
    public int Skill1Weapon {get {return skill1Slot;}}
    public int Skill2Weapon {get {return skill2Slot;}}
    public int SupportWeapon { get { return supportSlot ; } }

}