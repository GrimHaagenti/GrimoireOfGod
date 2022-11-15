using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EntityStats", menuName ="BaseEntityStats")]
public class EntityStat : ScriptableObject
{
    public int currentHP = 0;
    public int MaxHP = 10;
    public int Atk = 0;
    public int Def = 0;
    public int Spd = 0;
    
}

