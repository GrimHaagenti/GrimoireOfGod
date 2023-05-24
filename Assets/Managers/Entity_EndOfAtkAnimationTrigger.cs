using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_EndOfAtkAnimationTrigger : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<New_Entity_Script>().AtkAnimationFinished?.Invoke();
    }
}
