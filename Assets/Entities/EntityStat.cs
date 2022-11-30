using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EntityStats", menuName ="Stats/BaseEntityStats")]
public class EntityStat : ScriptableObject
{
    public int MaxHP = 10;
    public int Atk = 0;
    public int Def = 0;
    public int Spd = 0;

    [SerializeField] public List<ElementalBlock> ElementInventory;
    [SerializeField] public List<Relic> RelicInventory;
    [SerializeField] public Attributes[] Weaknesses;


}

