using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RA_CastBarrier_Action", menuName = "RelicActions/Cast Barrier")]
public class CastBarrierAction : RelicAction
{
    [SerializeField] BarrierStats barrierStats;
    public override TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        return CastBarrier(targets, elementalBlock, user, RelicPotency, PreviousAttackResolution);
    }


    private TurnResolution CastBarrier(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        int newBaseHp = barrierStats.baseBarrierHP + ((elementalBlock.Potency + RelicPotency) * 20);
        int newElementalHp = barrierStats.elementalBarrierHP * ((elementalBlock.Potency + RelicPotency) * 20);

        Barrier newBarrier = new Barrier();

        newBarrier.CastBarrier(newBaseHp, newElementalHp, elementalBlock.BlockElement);
        PreviousAttackResolution.UserBarrier = newBarrier;

        return PreviousAttackResolution;

    }

}
