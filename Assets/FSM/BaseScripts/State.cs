using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/State")]
public class State : ScriptableObject
{
    

    [SerializeField] Action[] Actions;

    [SerializeField] Color sceneGizmoColor = Color.gray;

    [SerializeField] Transition[] Transitions;


    public void UpdateState(StateController controller)
    {
        DoActions(controller);
    }


    private void DoActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionSucceded = Transitions[i].decision.Decide(controller);

            if (decisionSucceded)
            {
                controller.TransitionToState(Transitions[i].trueState);

            }
        }
    }
    public Color StateGizmoColor { get { return sceneGizmoColor; } }
}
