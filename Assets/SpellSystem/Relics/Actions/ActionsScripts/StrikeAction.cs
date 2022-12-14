using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RA_NormalHit_Atk_Action", menuName = "RelicActions/Hit Atk")]
public class StrikeAction : RelicAction
{
    public override TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        return Hit(targets, elementalBlock, user, RelicPotency, PreviousAttackResolution);
    }

    private TurnResolution Hit(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {

        int DamageDealt = GameManager._GAME_MANAGER.CalculateBattleDamage(RelicPotency, user.CurrentATK, elementalBlock.Potency, targets[0].CurrentDEF);


        Debug.Log(elementalBlock.BlockElement);
        Debug.Log(elementalBlock.Level);
        Debug.Log(elementalBlock.Potency);

        PreviousAttackResolution.ApplyToTargets.Add(DamageDealt);
        PreviousAttackResolution.ElementsUsed.Add(elementalBlock.BlockElement);


        return PreviousAttackResolution;
    }
}
