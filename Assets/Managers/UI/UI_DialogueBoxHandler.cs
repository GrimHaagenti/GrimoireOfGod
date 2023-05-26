using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_DialogueBoxHandler : UI_ParentStandard
{
    [SerializeField] Image m_Portrait;
    [SerializeField] TextMeshProUGUI m_CharacterName;
    [SerializeField] TextMeshProUGUI m_Dialogue;

    List<Dialogue_Scr> m_currentConversation = null;
    int m_currentConversationIndex = 0;
    float scrollTimer = 0;
    float pressTimer = 0;
    float pressMaxTime = 1f;

    bool isTextScrolling = false;
    bool isButtonPressed = false;
    bool isDialoguePlaying = false;

    public UnityEvent OnBeginDialogue;
    public UnityEvent OnEndDialogue;

    public void Init()
    {
        ShowPanel(false);
    }
    

    public void BeginConversation(List<Dialogue_Scr> convo)
    {
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed.AddListener(GoToNextDialogue);
        m_currentConversation = convo;
        m_currentConversationIndex = 0;
        isDialoguePlaying = true;
        SetDialogueBox();

    }

    private void SetDialogueBox()
    {
            m_CharacterName.text = m_currentConversation[m_currentConversationIndex].m_CharacterName;
            m_Portrait.sprite = m_currentConversation[m_currentConversationIndex].m_characterPortrait;
            m_Dialogue.text = m_currentConversation[m_currentConversationIndex].m_dialogue;
            m_Dialogue.maxVisibleCharacters = 0;
            isTextScrolling = true;
        
    }

    void Update()
    {
        if (isTextScrolling)
        {
            scrollTimer += Time.deltaTime;

            if (scrollTimer >= m_currentConversation[m_currentConversationIndex].m_scrollSpeed)
            {
                scrollTimer = 0;
                m_Dialogue.maxVisibleCharacters++;
                if(m_Dialogue.maxVisibleCharacters >= m_Dialogue.text.Length)
                {
                    isTextScrolling = false;
                }
            }
        }
        if(isButtonPressed)
        {
            pressTimer += Time.deltaTime;
            if(pressTimer>= pressMaxTime) { isButtonPressed = false; pressTimer = 0; }
            
        }
    }
    private void GoToNextDialogue()
    {
        if (isDialoguePlaying)
        {
            if (!isButtonPressed)
            {
                if (m_Dialogue.maxVisibleCharacters < m_Dialogue.text.Length)
                {
                    m_Dialogue.maxVisibleCharacters = m_Dialogue.text.Length;
                    isTextScrolling = false;
                    scrollTimer = 0;
                    isButtonPressed = true;

                }
                else
                {
                    isButtonPressed = true;
                    isTextScrolling = false;
                    m_currentConversation[m_currentConversationIndex].dialogInteraction?.DoSomething();
                    m_currentConversationIndex++;
                    if (m_currentConversationIndex >= m_currentConversation.Count)
                    {
                        EndConversation();
                        return;
                    }
                    SetDialogueBox();
                }
            }
        }
    }

    private void EndConversation()
    {
        OnEndDialogue?.Invoke();
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed.RemoveListener(GoToNextDialogue);
        isDialoguePlaying = false;
        ShowPanel(false);
        InputManager._INPUT_MANAGER.SetInputToWorld();
    }

}
