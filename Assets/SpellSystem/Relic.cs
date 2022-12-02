using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Relic", menuName ="New Relic")]
public class Relic : ScriptableObject
{
    [SerializeField] protected Sprite runeIcon;
    [SerializeField] protected string name;
    [SerializeField] private RelicAction[] effects;

    [SerializeField] public List<ElementalBlock> relicsElement = new List<ElementalBlock>();


    public TurnResolution Use(List<Entity> targets, Entity user)
    {
        TurnResolution resolution = new TurnResolution();
        int previousAttackResolution = 0;
        for (int i  = 0; i < effects.Length; i++)
        {
            previousAttackResolution = effects[i].Use(targets, relicsElement, user, resolution);
        }

        return resolution;
    }
    public bool AddToRelicElements(Elements element, int Level)
    {
        if (Level-1 > (int)ElementLevel.THREE) { return false; }
        if (relicsElement.Count > 3) { return false; }

        ElementalBlock newElem = new ElementalBlock();
        newElem.BlockElement = element;
        newElem.Quantities[Level - 1]++;

        relicsElement.Add(newElem);

        return true;

    }
    
    public Sprite RuneIcon { get { return runeIcon; } }
}
