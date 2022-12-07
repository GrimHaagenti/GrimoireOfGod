using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //UI Indexer
    InputManager input;
    Panel currentPanel;
    Stack<Panel> PanelStack;

    PanelIndexer indexer;

    public delegate void AcceptPressed();
    public AcceptPressed OnAcceptPressed;

    public void Init()
    {
        input = GameManager._GAME_MANAGER._INPUT_MANAGER;
        

    }
    public void OnTurnBegin()
    {
        if(currentPanel != null)
        {
            currentPanel.gameObject.SetActive(true);
        }
    }
    public void SetIndexer(PanelIndexer ind)
    {
        indexer = ind;
        currentPanel = indexer.EntryPanel;
        ChangePanel(currentPanel);
        currentPanel.OnEnterPanel();
    }
    void ChangePanel(Panel nextPanel)
    {
        nextPanel.OnMovePanelForward += MovePanelForward;
    }

    public void MovePanelForward(Panel nextPanel)
    {
        currentPanel.OnExitPanel();
        currentPanel.gameObject.SetActive(false);
        
        nextPanel.gameObject.SetActive(true);
        //nextPanel.OnEnterPanel();
        
    }

    public void Update()
    {
        if (GameManager._GAME_MANAGER._INPUT_MANAGER.AcceptButtonPressed)
        {
            OnAcceptPressed.Invoke();
        }
    }

}
