using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInfoUI : Panel 
{

    private ElementalBlock[] PlayerElementalBlocks;
    private Relic[] playerRelics;
    public MainMenuTextIndex index;

    public override void SetSubmenu()
    {
        index.PlayerName.text = "Name: " + GameManager._GAME_MANAGER.playerScript.name;
        index.PlayerHP.text = "HP: " + GameManager._GAME_MANAGER.playerScript.CurrentHP + " / " + GameManager._GAME_MANAGER.playerScript.GetEntityStats.MaxHP;
        index.PlayerATK.text = "ATK: " + GameManager._GAME_MANAGER.playerScript.CurrentATK.ToString();
        index.PlayerDEF.text = "DEF: " + GameManager._GAME_MANAGER.playerScript.CurrentDEF.ToString();
        index.PlayerSPD.text = "SPD: " + GameManager._GAME_MANAGER.playerScript.CurrentSPD.ToString();
        index.MainRelic.sprite = GameManager._GAME_MANAGER.playerScript.MainRelic.RuneIcon;
        for (int q = 0; q < GameManager._GAME_MANAGER.playerScript._RelicInventory.Count; q++)
        {
            index.SubRelics[q].sprite = GameManager._GAME_MANAGER.playerScript._RelicInventory[q].RuneIcon;
        }

        for (int i = 0; i < (int)Elements.PHYSICAL; i++)
        {
            int counter = 0;
            for (int x = 0; x < GameManager._GAME_MANAGER.playerScript._ElementInventory.Count; x++)
            {
                if(GameManager._GAME_MANAGER.playerScript._ElementInventory[x].BlockElement == (Elements)i)
                {
                    counter++;
                }
            }
            index.ElementQuantity[i].text = "x" + counter;
        }
    }

    public override void OnEnterPanel()
    {
        SetSubmenu();

        base.OnEnterPanel();
        GameManager._GAME_MANAGER._UI_MANAGER.OnMenuPausePressed += ExitTree;
    }

    private void ExitTree()
    {
        GameManager._GAME_MANAGER._UI_MANAGER.ExitPanelTree();
    }
    public override void OnExitPanel()
    {
        base.OnExitPanel();
        panelFinishedLoading = true;
        GameManager._GAME_MANAGER._UI_MANAGER.OnMenuPausePressed -= ExitTree;

    }
}
