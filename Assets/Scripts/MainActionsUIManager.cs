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


    [SerializeField] protected Button BaseAtk;
    [SerializeField] protected Button Relics;
    [SerializeField] protected Button Book;

    int currentButton;

    [SerializeField] protected GameObject RelicsPanel;




    public void SetSubmenu()
    {


        Relics.onClick.AddListener(() =>
        {
            RelicsPanel.SetActive(true);
            gameObject.SetActive(false);
        });

    }

    public override void GoForward()
    {
        OnMovePanelForward.Invoke(ForwardPanel);
    }

    public override void GoBackwards()
    {
        currentButton = (int)ButtonOrder.GRIMOIRE;
        ForwardPanel = ForwardPanels[currentButton];
        GoForward();

    }

    public override void OnExitPanel()
    {
    }

    public override void OnEnterPanel()
    {
        GameManager._GAME_MANAGER._UI_MANAGER.OnAcceptPressed += OnAcceptButton;
    }

    public override void OnAcceptButton()
    {
        currentButton = (int)ButtonOrder.RELICS;
        ForwardPanel = ForwardPanels[currentButton];
        GoForward();

    }

    public override void OnHoldElementButton()
    {
        currentButton = (int)ButtonOrder.BASEATK;
        ForwardPanel = ForwardPanels[currentButton];
        GoForward();

    }

    public override void OnNavigationVertical(int dir)
    {
    }

    public override void OnNavigationHorizontal(int dir)
    {
    }
}
