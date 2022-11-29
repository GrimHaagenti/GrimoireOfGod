using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Decision/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        return false;
    }
}
