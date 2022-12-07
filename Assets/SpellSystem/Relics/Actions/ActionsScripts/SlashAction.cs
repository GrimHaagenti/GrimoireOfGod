using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName= "RA_IgnoreDefense_Atk_Action", menuName = "RelicActions/Ignore Defense Atk")]
public class SlashAction : RelicAction
{

    [SerializeField] private int[] IgnoreDefensePercentage = new int[3];

    public override TurnResolution Use(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        return Slash(targets, elementalBlock, user, RelicPotency, PreviousAttackResolution);
    }   

    private TurnResolution Slash(List<Entity> targets, ElementalBlock elementalBlock, Entity user, int RelicPotency, TurnResolution PreviousAttackResolution)
    {
        float percentageCalculation = IgnoreDefensePercentage[(int)elementalBlock.Level]/100;
        int TargetDefense = Mathf.CeilToInt(targets[0].CurrentDEF - (targets[0].CurrentDEF * percentageCalculation));


        int DamageDealt = GameManager._GAME_MANAGER.CalculateBattleDamage(RelicPotency, user.CurrentATK, elementalBlock.Potency, TargetDefense);


        Debug.Log(elementalBlock.BlockElement);
        Debug.Log(elementalBlock.Level);
        Debug.Log(elementalBlock.Potency);

        PreviousAttackResolution.ApplyToTargets.Add(DamageDealt);
        PreviousAttackResolution.ElementsUsed.Add(elementalBlock.BlockElement);


        return PreviousAttackResolution;


    }

}
