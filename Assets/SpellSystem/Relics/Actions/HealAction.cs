using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RelicActions/RA_HealAction")]
    public class HealAction : RelicAction
    {
        public override int Use(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user, int PreviousAttackResolution)
        {
            return Heal(targets, elementalBlocks, user, PreviousAttackResolution);
        }

        private int Heal(List<Entity> targets, List<ElementalBlock> elementalBlocks, Entity user, int PreviousAttackResolution)
        {

            return 0;
        }

    }