using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "RelicActions/RA_SlashAction")]
public class SlashAction : RelicAction
{
    public override int Use(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user, TurnResolution PreviousAttackResolution)
    {
        return Slash(targets, elementalBlocks, user, PreviousAttackResolution);
    }

    private int Slash(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user, TurnResolution PreviousAttackResolution)
    {

        return 1;

        
    }

}
