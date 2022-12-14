using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Player Spellsword Action")]
public class PlayerSpellswordActAction : Action
{
    Vector3 EndPosition = Vector3.zero;

    public override void Act(StateController controller)
    {
        QuickSlash(controller);
    }

    private void QuickSlash(StateController controller)
    {
        
        
        RaycastHit hit;
        
        Vector3 StartPosition = controller.gameObject.transform.position;

        int layerMask = (1 << 6) | (1 << 7);

        if (InputManager._INPUT_MANAGER.IsActionButtonPressed())
        { 
            EndPosition = StartPosition + controller.gameObject.transform.forward * controller.worldStats.SlashDistace;

            controller.worldStats.ActionInterrumptMovement = true;
        }

        if (controller.worldStats.ActionInterrumptMovement)
        {
            if (Vector3.Distance(StartPosition, EndPosition) >0.5f) 
            {

                controller.character.Move(((EndPosition - StartPosition) * controller.worldStats.SlashSpeed ) * Time.deltaTime);

            }
            else
            {
                controller.worldStats.ActionInterrumptMovement = false;
            }


        }
        Debug.DrawLine(StartPosition, EndPosition, Color.red);

        Physics.Linecast(StartPosition, EndPosition, out hit, layerMask);

    }

}
