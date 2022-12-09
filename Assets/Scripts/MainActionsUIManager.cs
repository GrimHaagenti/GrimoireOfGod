using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainActionsUIManager : Panel
{

    BattleManager battleManager;
    enum ButtonOrder { BASEATK, RELICS, GRIMOIRE };

    [SerializeField] List<Button> Buttons;
    [SerializeField] List<Panel> ForwardPanels;

    int currentButton;

    [SerializeField] protected GameObject RelicsPanel;

    public override void GoForward()
    {
        base.GoForward();

    }
    public override void GoBackButtonPressed()
    {
        currentButton = (int)ButtonOrder.GRIMOIRE;
        ForwardPanel = ForwardPanels[currentButton];
        Debug.Log("bbb");
        GoForward();
    }
  

    public override void OnExitPanel()
    {
        base.OnExitPanel();
    }


    public override void OnAcceptButton()
    {
        currentButton = (int)ButtonOrder.RELICS;
        ForwardPanel = ForwardPanels[currentButton];
        Debug.Log("AAAA");
        base.OnAcceptButton();

    }

    public override void OnHoldElementButton()
    {
        currentButton = (int)ButtonOrder.BASEATK;
        ForwardPanel = ForwardPanels[currentButton];
        GoForward();
        
    }

 
}
