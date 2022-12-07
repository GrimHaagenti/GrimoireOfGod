using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    public Panel ForwardPanel;
    public Panel BackPanel;

    public delegate void MovePanelForward(Panel nextPanel);
    public MovePanelForward OnMovePanelForward;

    public abstract void GoForward();
    public abstract void GoBackwards();
    public abstract void OnExitPanel();
    public abstract void OnEnterPanel();
    public abstract void OnAcceptButton();
    public abstract void OnHoldElementButton();
    public abstract void OnNavigationVertical(int dir);
    public abstract void OnNavigationHorizontal(int dir);


}
