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
        


        GameManager._GAME_MANAGER.playerScript._RelicInventory.ForEach(
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
            GameManager._GAME_MANAGER.playerScript._RelicInventory[buttonNavIndex] = newRelic;
        }
        GameManager._GAME_MANAGER._INPUT_MANAGER.ChangeInputType(Scenes.WORLD);
        gameObject.SetActive(false);

    }

    public override void OnEnterPanel()
    {

        base.OnEnterPanel();
        SetSubmenu();
        RelicButtonUIList[buttonNavIndex].Select();

    }

    public override void OnNavigationVertical(int dir)
    {
        if(buttonNavIndex< RelicButtonUIList.Count - 1)
        {
            buttonNavIndex = RelicButtonUIList.Count - 1;
        }
        else { buttonNavIndex = 0; }

        RelicButtonUIList[buttonNavIndex].Select();
    }

    public override void OnNavigationHorizontal(int dir)
    {
        buttonNavIndex += dir;
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, RelicButtonUIList.Count - 2);

        RelicButtonUIList[buttonNavIndex].Select();

    }
}
