using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractionScript : MonoBehaviour
{

    [SerializeField] WorldInteractIE ObjectToTrigger;
    [SerializeField] int playerLayer = 3;
    
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            if (InputManager._INPUT_MANAGER.Exploration_GetActionButtonPressed())
            {

                ObjectToTrigger.Interact();

            }
        }
    }
}
