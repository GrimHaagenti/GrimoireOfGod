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


    public void Use(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user)
    {
        int previousAttackResolution = 0;
        for (int i  = 0; i < effects.Length; i++)
        {
            previousAttackResolution = effects[i].Use(targets, elementalBlocks, user, previousAttackResolution);
        }

        
    }
    
    public Sprite RuneIcon { get { return runeIcon; } }
}
