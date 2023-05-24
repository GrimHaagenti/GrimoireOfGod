using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ItemDB", menuName ="New/Item DB Asset")]
public class ItemDB : ScriptableObject
{
    [SerializeField] private List<Weapon_Scr> weapons;

    public List<Weapon_Scr> Weapons { get { return weapons; } }

} 
