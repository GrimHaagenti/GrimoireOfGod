using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleDialogueInteraction : WorldInteractIE
{
    [SerializeField] Conversation_Scr Conversation;
    public override void Interact()
    {
        New_UI_Manager._UI_MANAGER.ShowDialog(Conversation.m_permanentConversation);
        gameObject.SetActive(false);
    }
}
