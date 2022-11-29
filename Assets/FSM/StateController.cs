using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    public State currentState;


    [SerializeField] bool Active;
    public CharacterController character { get; private set; }
    [SerializeField] public EntityWorldStats worldStats;
    


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        worldStats.SlashSpeed = worldStats.SlashDistace / worldStats.SlashTime;
        worldStats.ActionInterrumptMovement = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active) { return; }
        currentState.UpdateState(this);
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
