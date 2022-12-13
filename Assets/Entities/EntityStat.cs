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
    /// <summary>
    /// Limit of the Element Inventory per Element
    /// </summary>
    public int ElementInvetoryLimit = 20;
    /// <summary>
    /// Limit of the Relics the Entity can carry
    /// </summary>
    public int RelicMaxLimit = 3;

    [SerializeField] public ElementalBlock[] ElementInventory;
    [SerializeField] public Relic[] RelicInventory;
    [SerializeField] public Attributes[] Weaknesses;


}

