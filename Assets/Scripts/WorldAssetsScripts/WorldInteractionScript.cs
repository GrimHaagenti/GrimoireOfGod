using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldInteractionScript : MonoBehaviour
{

    [SerializeField] WorldInteractIE ObjectToTrigger;
    [SerializeField] float maxCastDistance;
    [SerializeField] int levelLayer;
    [SerializeField] int DetectCone = 30;
    float SphereCastSize = 6f;
    private void OnTriggerStay(Collider other)
    {
        ;

        if (InputManager._INPUT_MANAGER.IsActionButtonPressed())
        {
            
            ObjectToTrigger.Interact();
            
        }
    }
}
