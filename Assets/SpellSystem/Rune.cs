using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="Rune", menuName ="New Relic" )]
public class Rune : ScriptableObject
{
    [SerializeField] protected Sprite runeIcon;
    [SerializeField] protected string name;
    [SerializeField] private Effect effect = Effect.PROJECTILE;


    public virtual void Use(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user)
    {
        switch (effect)
        {
            case Effect.PROJECTILE:
                RuneEffects.Projectile(targets, elementalBlocks, user);
                break;
            case Effect.BLADE:
                RuneEffects.Slash(targets, elementalBlocks, user);
                break;
            case Effect.BARRIER:
                break;
            case Effect.EXPLOSION:
                break;
        };
    }



    public Sprite RuneIcon { get { return runeIcon; } }
}
