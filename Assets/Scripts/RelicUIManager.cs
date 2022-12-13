using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RelicUIManager : Panel
{
    BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    //[SerializeField] protected List<Button> optionButtons;



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
                    RelicButtonAccessor relicButtonAccessor = buttonObject.GetComponent<RelicButtonAccessor>();
                    relicButtonAccessor.relicName.text = it.relicName;
                    relicButtonAccessor.relicDescription.text = "";
                    foreach (RelicAction act in it.effects) {

                        relicButtonAccessor.relicDescription.text += act.Description +" ";
                    }
                    relicButtonAccessor.relicIcon.sprite = it.runeIcon;

                    Button button = buttonObject.GetComponent<Button>();

                    RelicButtonUIList.Add(button);
                    RelicBlockUIList.Add(buttonObject);
                }
                );
            RelicButtonUIList[buttonNavIndex].Select();

        }

    }

    public void SetActiveRune(Relic rune)
    {
        activeRune = rune;
    }

    public override void OnAcceptButton()
    {
        if(buttonNavIndex <= RelicButtonUIList.Count - 1)
        {
            Debug.Log("chose Relic");
            SetActiveRune( playerRunes[buttonNavIndex]);
            battleManager.SetRune(activeRune);
            base.OnAcceptButton();
            
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
        base.OnExitPanel();
    }
    public override void OnHoldElementButton()
    {
    }

    public override void OnNavigationVertical(int dir)
    {
        buttonNavIndex -= dir;
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, RelicButtonUIList.Count - 1);

        RelicButtonUIList[buttonNavIndex].Select();
    }

    public override void OnNavigationHorizontal(int dir)
    {
        

    }

}
