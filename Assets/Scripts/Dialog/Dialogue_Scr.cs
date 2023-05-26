using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Dialogue", menuName = "New/Dialogue")]
public class Dialogue_Scr : ScriptableObject
{
    [SerializeField] public string m_CharacterName;
    [SerializeField] public Sprite m_characterPortrait;
    [TextArea(4,4)]
    [SerializeField] public string m_dialogue;
    [SerializeField] public float m_scrollSpeed = 0.05f;
    [SerializeField] public DialogueInteractionIE dialogInteraction;
}
