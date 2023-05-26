using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDialogueInteraction : WorldInteractIE
{
    [SerializeField] Conversation_Scr Conversation;
    [SerializeField] bool firstTime = true;
    public override void Interact()
    {
        if (firstTime && Conversation.m_firstTimeConversation.Count> 0)
        {
            New_UI_Manager._UI_MANAGER.ShowDialog(Conversation.m_firstTimeConversation);
            firstTime = false;
        }
        else
        {
            New_UI_Manager._UI_MANAGER.ShowDialog(Conversation.m_permanentConversation);
        }
    }
}
