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
        if (Old_GameManager._GAME_MANAGER.playerScript._ElementInventory.Count <= 0)
        {
            Old_GameManager._GAME_MANAGER.ShowMessage("NO ELEMENTS IN INVENTORY", 5f);
            return;
        }
        currentButton = (int)ButtonOrder.RELICS;
        ForwardPanel = ForwardPanels[currentButton];
        base.OnAcceptButton();

    }

    public override void OnHoldElementButton()
    {
        Old_GameManager._GAME_MANAGER._BATTLE_MANAGER.SetPlayerDefaultAtk();
        Old_GameManager._GAME_MANAGER._BATTLE_MANAGER.ChangeState(BattleStates.PLAYER_RESOLUTION);
        currentButton = (int)ButtonOrder.BASEATK;
        ForwardPanel = ForwardPanels[currentButton];
        
    }

 
}
