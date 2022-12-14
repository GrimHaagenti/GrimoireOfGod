using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Panel ForwardPanel;
    public Panel BackPanel;

    public delegate void MovePanelForward(Panel nextPanel);
    public MovePanelForward OnMovePanelForward;

    public delegate void MovePanelBackwards();
    public MovePanelBackwards OnMovePanelBackwards;
    public bool panelFinishedLoading = false;


    public virtual void SetSubmenu()
    {

    }

    public virtual void GoForward()
    {
        if(ForwardPanel == null || !panelFinishedLoading) { return; }
        OnMovePanelForward.Invoke(ForwardPanel);
    }

    private void SortInput(Vector2 input)
    {

        if (input.x != 0)
        {
            OnNavigationHorizontal(Mathf.FloorToInt(input.x));
        }
        if (input.y != 0)
        {
            OnNavigationVertical(Mathf.FloorToInt(input.y));
        }

    }
    public virtual void GoBackButtonPressed()
    {
        GoBackwards();
    }
    public virtual void GoBackwards()
    {
        OnMovePanelBackwards.Invoke();
    }
    public virtual void OnExitPanel()
    {
        GameManager._GAME_MANAGER._UI_MANAGER.OnAcceptPressed -= OnAcceptButton;
        GameManager._GAME_MANAGER._UI_MANAGER.OnGoBackPressed -= GoBackButtonPressed;
        GameManager._GAME_MANAGER._UI_MANAGER.OnHoldElementPressed -= OnHoldElementButton;
        GameManager._GAME_MANAGER._UI_MANAGER.OnNavigateAxis -= SortInput;
        panelFinishedLoading = false;
    }
    public virtual void OnEnterPanel()
    {

        GameManager._GAME_MANAGER._UI_MANAGER.OnAcceptPressed += OnAcceptButton;
        GameManager._GAME_MANAGER._UI_MANAGER.OnGoBackPressed += GoBackButtonPressed;
        GameManager._GAME_MANAGER._UI_MANAGER.OnHoldElementPressed += OnHoldElementButton;
        GameManager._GAME_MANAGER._UI_MANAGER.OnNavigateAxis += SortInput;
        panelFinishedLoading = true;
    }
    public virtual void OnAcceptButton()
    {
        GoForward();
    }
    public virtual void OnHoldElementButton() { }
    public virtual void OnNavigationVertical(int dir) { }
    public virtual void OnNavigationHorizontal(int dir) { }


}
