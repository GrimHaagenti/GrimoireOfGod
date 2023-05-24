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
        index.PlayerName.text = "Name: " + Old_GameManager._GAME_MANAGER.playerScript.name;
        index.PlayerHP.text = "HP: " + Old_GameManager._GAME_MANAGER.playerScript.CurrentHP + " / " + Old_GameManager._GAME_MANAGER.playerScript.GetEntityStats.MaxHP;
        index.PlayerATK.text = "ATK: " + Old_GameManager._GAME_MANAGER.playerScript.CurrentATK.ToString();
        index.PlayerDEF.text = "DEF: " + Old_GameManager._GAME_MANAGER.playerScript.CurrentDEF.ToString();
        index.PlayerSPD.text = "SPD: " + Old_GameManager._GAME_MANAGER.playerScript.CurrentSPD.ToString();
        index.MainRelic.sprite = Old_GameManager._GAME_MANAGER.playerScript.MainRelic.RuneIcon;
        for (int q = 0; q < Old_GameManager._GAME_MANAGER.playerScript._RelicInventory.Count; q++)
        {
            index.SubRelics[q].sprite = Old_GameManager._GAME_MANAGER.playerScript._RelicInventory[q].RuneIcon;
        }

        for (int i = 0; i < (int)Elements.PHYSICAL; i++)
        {
            int counter = 0;
            for (int x = 0; x < Old_GameManager._GAME_MANAGER.playerScript._ElementInventory.Count; x++)
            {
                if(Old_GameManager._GAME_MANAGER.playerScript._ElementInventory[x].BlockElement == (Elements)i)
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
        Old_GameManager._GAME_MANAGER._UI_MANAGER.OnMenuPausePressed += ExitTree;
    }

    private void ExitTree()
    {
        Old_GameManager._GAME_MANAGER._UI_MANAGER.ExitPanelTree();
    }
    public override void OnExitPanel()
    {
        base.OnExitPanel();
        panelFinishedLoading = true;
        Old_GameManager._GAME_MANAGER._UI_MANAGER.OnMenuPausePressed -= ExitTree;

    }
}
