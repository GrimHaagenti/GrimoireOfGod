using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRelicUI : Panel
{
    [SerializeField] GameObject RelicBlockUI;

    [SerializeField] public GameObject PlayerRelicParent;
    [SerializeField] public Button NewRelicButton;
    [SerializeField] public Button DeclineButton;

    public Relic newRelic;

    List<GameObject> RelicBlockUIList = new List<GameObject>();
    List<Button> RelicButtonUIList = new List<Button>();


    int buttonNavIndex = 0;

    public override void SetSubmenu()
    {
        RelicButtonAccessor relicButtonAccessor = NewRelicButton.GetComponent<RelicButtonAccessor>();
        relicButtonAccessor.relicName.text = newRelic.relicName;
        relicButtonAccessor.relicDescription.text = "";
        foreach (RelicAction act in newRelic.effects)
        {

            relicButtonAccessor.relicDescription.text += act.Description + " ";
        }
        relicButtonAccessor.relicIcon.sprite = newRelic.runeIcon;
        


        Old_GameManager._GAME_MANAGER.playerScript._RelicInventory.ForEach(
                (it) =>
                {

                    GameObject buttonObject = Instantiate(RelicBlockUI, PlayerRelicParent.transform);
                    RelicButtonAccessor relicButtonAccessor = buttonObject.GetComponent<RelicButtonAccessor>();
                    relicButtonAccessor.relicName.text = it.relicName;
                    relicButtonAccessor.relicDescription.text = "";
                    foreach (RelicAction act in it.effects)
                    {

                        relicButtonAccessor.relicDescription.text += act.Description + " ";
                    }
                    relicButtonAccessor.relicIcon.sprite = it.runeIcon;

                    Button button = buttonObject.GetComponent<Button>();

                    RelicButtonUIList.Add(button);
                    RelicBlockUIList.Add(buttonObject);
                }
                );
        RelicButtonUIList.Add(DeclineButton);
        RelicBlockUIList.Add(DeclineButton.gameObject);
        RelicButtonUIList[buttonNavIndex].Select();


    }
    public override void OnAcceptButton()
    {
        if (buttonNavIndex <= RelicButtonUIList.Count - 2)
        {
            Old_GameManager._GAME_MANAGER.playerScript._RelicInventory[buttonNavIndex] = newRelic;
        }
        //InputManager._INPUT_MANAGER.ChangeInputType(Scenes.WORLD);
        base.OnAcceptButton();
    }


    public override void GoForward()
    {
        for (int i = 0; i < RelicBlockUIList.Count; i++)
        {
            if(i < RelicBlockUIList.Count-1)
            {
                Destroy(RelicBlockUIList[i]);
            }
        }
        RelicBlockUIList.Clear();
        RelicButtonUIList.Clear();
        Old_GameManager._GAME_MANAGER._UI_MANAGER.ExitPanelTree();

    }


    public override void OnExitPanel()
    {
        buttonNavIndex = 0;

    }

    public override void OnEnterPanel()
    {

        base.OnEnterPanel();
        RelicButtonUIList[buttonNavIndex].Select();

    }

    public override void OnNavigationVertical(int dir)
    {

        if(dir > 0)
        {
            if(buttonNavIndex == RelicBlockUIList.Count - 1)
            {
                buttonNavIndex = 0;
            }

        }
        else if (dir < 0)
        {
            if(buttonNavIndex <= RelicBlockUIList.Count - 2)
            {
                buttonNavIndex = RelicBlockUIList.Count - 1;
            }
        }
        RelicButtonUIList[buttonNavIndex].Select();
    }

    public override void OnNavigationHorizontal(int dir)
    {
        if (buttonNavIndex > 0|| buttonNavIndex <= RelicBlockUIList.Count - 2)
        { buttonNavIndex += dir; }
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, RelicButtonUIList.Count - 2);

        RelicButtonUIList[buttonNavIndex].Select();

    }
}
