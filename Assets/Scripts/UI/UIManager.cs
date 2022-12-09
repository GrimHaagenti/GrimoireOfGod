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

    public delegate void GoBackPressed();
    public GoBackPressed OnGoBackPressed;

    public delegate void HoldElementPressed();
    public HoldElementPressed OnHoldElementPressed;

    public delegate void NavigateAxis(Vector2 input);
    public NavigateAxis OnNavigateAxis;

    public void Init()
    {
        input = GameManager._GAME_MANAGER._INPUT_MANAGER;
        PanelStack = new Stack<Panel>();

    }
    public void OnTurnBegin()
    {
        currentPanel = indexer.PanelsInTree[0];
        foreach (Panel p in indexer.PanelsInTree)
        {
            p.gameObject.SetActive(false);
        }
        ChangePanel(currentPanel);
        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();
    }

    //Set the UI Panel Indexer(Entry Point for the UI Manager)
    //Once set the UI should function independently from the Panel it receives
    //Even custom Panels 
    public void SetIndexer(PanelIndexer ind)
    {

        indexer = ind;
        foreach (Panel p in indexer.PanelsInTree)
        {
            p?.SetSubmenu();

        }

        currentPanel = indexer.EntryPanel;
        ChangePanel(currentPanel);
        currentPanel.OnEnterPanel();
    }
    void ChangePanel(Panel nextPanel)
    {
        //Deactivate Previous Panel Events
        currentPanel.OnMovePanelForward -= MovePanelForward;

        //Activate New Panel Events
        nextPanel.OnMovePanelForward += MovePanelForward;
        nextPanel.OnMovePanelBackwards += MovePanelBackwards;
    }

    public void MovePanelBackwards()
    {
        if (!PanelStack.TryPeek(out Panel lastPanel)) {return;  }

        currentPanel.OnExitPanel();
        currentPanel.gameObject.SetActive(false);


        lastPanel = PanelStack.Pop();
        ChangePanel(lastPanel);
        currentPanel = lastPanel;

        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();

    }

    public void MovePanelForward(Panel nextPanel)
    {
        currentPanel.OnExitPanel();
        currentPanel.gameObject.SetActive(false);


        ChangePanel(nextPanel);
        PanelStack.Push(currentPanel);
        currentPanel = nextPanel;

        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();

    }

    public void Update()
    {
        if (currentPanel != null && currentPanel.panelFinishedLoading)
        {
            if (input.AcceptButtonPressed)
            {
                OnAcceptPressed.Invoke();
            }
            if (input.GoBackButtonPressed)
            {
                OnGoBackPressed.Invoke();
            }
            if (input.HoldElementButtonPressed)
            {
                OnHoldElementPressed.Invoke();
            }
            if (input.NavigateInput != Vector2.zero)
            {
                OnNavigateAxis.Invoke(input.NavigateInput);
            }
        }

    }
}
