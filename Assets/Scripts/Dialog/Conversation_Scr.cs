using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New_Conversation", menuName = "New/Conversation")]
public class Conversation_Scr : ScriptableObject
{
    [SerializeField] public List<Dialogue_Scr> m_firstTimeConversation;
    [SerializeField] public List<Dialogue_Scr> m_permanentConversation;
}
