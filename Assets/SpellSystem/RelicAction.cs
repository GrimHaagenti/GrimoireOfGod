using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



abstract public class RelicAction: ScriptableObject
{
    public abstract int Use(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user, TurnResolution PreviousAttackResolution);
    

}
