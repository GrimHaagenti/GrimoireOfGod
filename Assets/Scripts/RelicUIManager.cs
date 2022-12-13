using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUIManager : Panel
{
    BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    //[SerializeField] protected List<Button> optionButtons;

    [SerializeField] protected Button AdvanceMenuButton;


    [SerializeField] GameObject RelicBlockUI;
    [SerializeField] GameObject listParent;
    [SerializeField] Sprite DefaultSprite;

    List<GameObject> RelicBlockUIList = new List<GameObject>();
    List<Button> RelicButtonUIList = new List<Button>();

    int buttonNavIndex = 0;

    List<Relic> playerRunes;
    Relic activeRune;
    int MaxRelicNumber = 3;

    public override void SetSubmenu()
    {
        battleManager = GameManager._GAME_MANAGER._BATTLE_MANAGER;
        if (playerRunes == null)
        {
            playerRunes = battleManager.player._RelicInventory;

            //CHANGE TO MAX RELIC VARIABLE

            if (playerRunes.Count > MaxRelicNumber)
            {
                Debug.Log("Tooo many Relics");
                return;
            }

            playerRunes?.ForEach(
                (it) =>
                {

                    GameObject buttonObject = Instantiate(RelicBlockUI, listParent.transform);
                    buttonObject.GetComponentInChildren<Image>().sprite = it.RuneIcon;
                    Button button = buttonObject.GetComponentInChildren<Button>();

                // button.onClick.AddListener(() => SetActiveRune(it));

                RelicButtonUIList.Add(button);
                    RelicBlockUIList.Add(buttonObject);
                }
                );

            RelicButtonUIList.Add(AdvanceMenuButton);
        }
        RelicButtonUIList[buttonNavIndex].Select();

    }

    public void SetActiveRune(Relic rune)
    {
        AdvanceMenuButton.GetComponentInChildren<Image>().sprite = rune.RuneIcon;
        activeRune = rune;
    }

    public override void OnAcceptButton()
    {
        if(buttonNavIndex <= RelicButtonUIList.Count - 2)
        {
            Debug.Log("chose Relic");
            SetActiveRune( playerRunes[buttonNavIndex]);
        }
        else if (buttonNavIndex == RelicButtonUIList.Count - 1) {
            if (activeRune != null)
            {
                Debug.Log("Relic Chosen");
                battleManager.SetRune(activeRune);
                base.OnAcceptButton();
            }
        }

    }
    public override void OnEnterPanel()
    {

        base.OnEnterPanel();
        RelicButtonUIList[buttonNavIndex].Select();

    }
    public override void OnExitPanel()
    {
        //ERASE PROPERLY THE RELIC
        buttonNavIndex = 0;
        activeRune = null;
        AdvanceMenuButton.GetComponentInChildren<Image>().sprite = DefaultSprite;
        base.OnExitPanel();
    }
    public override void OnHoldElementButton()
    {
    }

    public override void OnNavigationVertical(int dir)
    {
    }

    public override void OnNavigationHorizontal(int dir)
    {
        buttonNavIndex += dir;
        buttonNavIndex = Mathf.Clamp(buttonNavIndex,0, RelicButtonUIList.Count - 1);

        RelicButtonUIList[buttonNavIndex].Select();

    }

}
