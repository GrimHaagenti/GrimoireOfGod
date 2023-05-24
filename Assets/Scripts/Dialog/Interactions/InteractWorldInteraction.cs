using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldInteract_IE", menuName = "New/DialogueInteraction/WorldInteraction")]
public class InteractWorldInteraction : DialogueInteractionIE
{
    [SerializeField] int interactChannel = -1;

    public override void DoSomething()
    {
        WorldInteract();
    }

    private void WorldInteract()
    {
        WorldInteractionManager._INTERACT_MANAGER.DeactivateObjectByChannel(interactChannel);
    }
}
