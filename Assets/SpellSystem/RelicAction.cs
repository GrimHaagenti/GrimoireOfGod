using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



abstract public class RelicAction: ScriptableObject
{
    public abstract TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution);

}
