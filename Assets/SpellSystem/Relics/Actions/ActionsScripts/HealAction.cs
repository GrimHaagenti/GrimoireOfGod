using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RelicActions/RA_HealAction")]
    public class HealAction : RelicAction
    {
        [SerializeField] private int BaseHealAmount;
        public override TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
        {
            return Heal(targets, elementalBlock, user, RelicPotency, PreviousAttackResolution);
        }

        private TurnResolution Heal(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
        {
        int healAmount = BaseHealAmount * RelicPotency;
        PreviousAttackResolution.ApplyToUser.Add(healAmount);

            return PreviousAttackResolution;
        }

    }