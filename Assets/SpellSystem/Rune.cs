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


    public virtual void Use(List<Entity> targets)
    {
        switch (effect)
        {
            case Effect.PROJECTILE:
                RuneEffects.Projectile(targets);
                break;
            case Effect.BLADE:
                RuneEffects.Slash(targets);
                break;
            case Effect.BARRIER:
                break;
            case Effect.EXPLOSION:
                break;
        };
    }



    public Sprite RuneIcon { get { return runeIcon; } }
}
