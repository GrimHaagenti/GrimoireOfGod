using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RA_Vampirism_Heal_Action", menuName = "RelicActions/Vampiric Heal")]
public class VampirismAction : RelicAction
{
    [SerializeField] private int[] VampirismPercentage = new int[3];

    public override TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        return Vampirism(targets, elementalBlock, user, RelicPotency, PreviousAttackResolution);
    }

    private TurnResolution Vampirism(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        float percentageCalculation = VampirismPercentage[(int)elementalBlock.Level] / 100;
        int DamageSum = 0 ;
        PreviousAttackResolution.ApplyToTargets.ForEach((it) =>
        {
            DamageSum += it;
        });

        int AmountToHeal = Mathf.CeilToInt(DamageSum * percentageCalculation);


        PreviousAttackResolution.ApplyToUser.Add(AmountToHeal);


        return PreviousAttackResolution;


    }

}
