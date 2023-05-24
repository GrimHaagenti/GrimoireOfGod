using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[System.Serializable]
public class UIManager
{
    //UI Indexer
    InputManager input;
    Panel currentPanel;
    Stack<Panel> PanelStack;

   public PanelIndexer indexer;

    float MessageTimer = 0;
    float MessageDuration = 0;
    public bool isOnBattle = false;
    public TextMeshProUGUI messageObj;


    //Events
    public delegate void AcceptPressed();
    public AcceptPressed OnAcceptPressed;

    public delegate void GoBackPressed();
    public GoBackPressed OnGoBackPressed;

    public delegate void HoldElementPressed();
    public HoldElementPressed OnHoldElementPressed;
    
    public delegate void PausePressed();
    public PausePressed OnPausePressed;

    public delegate void NavigateAxis(Vector2 input);
    public NavigateAxis OnNavigateAxis;
    
    public delegate void MenuPausePressed();
    public MenuPausePressed OnMenuPausePressed;

    public void Init()
    {
        input = InputManager._INPUT_MANAGER;
        PanelStack = new Stack<Panel>();

    }

    public void ChangeRelicPrompt(Relic relic)
    {

        indexer.changeRelicUI.newRelic = relic;
        EnterPanelTree(indexer.changeRelicUI);

    }
    public void EnterPanelTree()
    {
        PanelStack.Clear();
        currentPanel = indexer.PanelsInTree[0];
        foreach (Panel p in indexer.PanelsInTree)
        {
            p.gameObject.SetActive(false);
        }
        ChangePanel(currentPanel);
        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();
    }
    public void EnterPanelTree(Panel entry)
    {
        PanelStack.Clear();
        currentPanel = entry;
        if (!indexer.PanelsInTree.Contains(currentPanel)) { currentPanel.SetSubmenu(); }
        foreach (Panel p in indexer.PanelsInTree)
        {
            p.SetSubmenu();
            p.gameObject.SetActive(false);
        }
        ChangePanel(currentPanel);
        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();
    }
    public void ExitPanelTree()
    {
        currentPanel.OnExitPanel();
        currentPanel.gameObject.SetActive(false);
        foreach (Panel p in indexer.PanelsInTree)
        {
            p.gameObject.SetActive(false);
        }
        //InputManager._INPUT_MANAGER.ChangeInputType(Scenes.WORLD);

    }

    /// <summary>
    ///Set the UI Panel Indexer(Entry Point for the UI Manager)
    ///Once set the UI should function independently from the Panel it receives
    ///Even custom Panels  
    /// </summary>
    /// <param name="ind">The Indexer with the Entry point to the panel tree </param>
    public void SetIndexer(PanelIndexer ind)
    {

        indexer = ind;
        messageObj = indexer.messageObj;
        currentPanel = indexer.EntryPanel;
        ChangePanel(currentPanel);
        if (input.GetInputType() == Scenes.BATTLE)
        {
            currentPanel.OnEnterPanel();
        }
        else { currentPanel.panelFinishedLoading = true; }
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
        currentPanel.SetSubmenu();

        currentPanel.gameObject.SetActive(true);
        currentPanel.OnEnterPanel();

    }

    public void Update()
    {
        if (currentPanel != null && currentPanel.panelFinishedLoading)
        {
            if (input.Menu_GetAcceptButtonPressed())
            {
                OnAcceptPressed?.Invoke();
            }
            if (input.Menu_GetGoBackButtonPressed())
            {
                OnGoBackPressed?.Invoke();
            }
            
            if (input.Menu_GetNavigateInput() != Vector2.zero)
            {
                //OnNavigateAxis?.Invoke(input.MenuNavigateInput);
            }

            if (input.Exploration_GetPauseButtonPressed())
            {
                EnterPanelTree();
                //InputManager._INPUT_MANAGER.ChangeInputType(Scenes.BATTLE);
            }

            if (input.Menu_GetPauseButtonPressed())
            {
                
                OnMenuPausePressed?.Invoke();
            }
            

        }




        if (messageObj != null && messageObj.gameObject.activeSelf)
        {
            if (MessageTimer < MessageDuration)
            {
                MessageTimer += Time.deltaTime;
            }
            else { MessageDuration = 0; MessageTimer = 0; messageObj.gameObject.SetActive(false); }
        }
    }
    public void ShowMessage(string message, float duration)
    {
        MessageTimer = 0;
        MessageDuration = duration;
        messageObj.text = message;
        messageObj.gameObject.SetActive(true);
    }

}
