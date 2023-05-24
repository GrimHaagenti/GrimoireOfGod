using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour 
{

    public State currentState;


    [SerializeField] bool Active;
    public CharacterController character { get; private set; }
    [SerializeField] public EntityWorldStats worldStats;
    [SerializeField] public Animator animator;


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        //worldStats.SlashSpeed = worldStats.SlashDistace / worldStats.SlashTime;
        //worldStats.ActionInterrumptMovement = false;
    }

    void Update()
    {
        if (!Active) { return; }
        currentState.UpdateState(this);
    }
    public void PlayAnimation(EntityStates newState)
    {
        switch (newState)
        {
            case EntityStates.IDLE:
                animator.SetBool("Moving", false);
                break;
            case EntityStates.MOVE:
                animator.SetBool("Moving", true);
                break;
            case EntityStates.NONE_STATE:
                break;
        }
    }


    private void OnDrawGizmos()
    {
        if(currentState!= null)
        {
            Gizmos.color = currentState.StateGizmoColor;
            Gizmos.DrawWireCube(gameObject.transform.position, Vector3.one*3);
        }
    }


    public void TransitionToState(State nextState)
    {
        if (nextState != currentState)
        {
            currentState = nextState;
        }
    }
}
