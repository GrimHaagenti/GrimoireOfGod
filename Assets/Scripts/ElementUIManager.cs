using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUIManager : Panel
{
    BattleManager battleManager;

    [SerializeField] protected Button AdvanceMenuButton;

    [SerializeField] GameObject ElementalBlockUI;
    [SerializeField] GameObject listParent;

    List<GameObject> elementalBlockUIList = new List<GameObject>();
    List<ElementalBlock> playerElements;
    List<ElementalBlock> activeElements = new List<ElementalBlock>();
    
    List<Button> ElementButtonUIList = new List<Button>();

    int buttonNavIndex = 0;
    int lastVerticalIndex = 0;

    public override void SetSubmenu()
    {
        battleManager = GameManager._GAME_MANAGER._BATTLE_MANAGER;
        playerElements = battleManager.player.EntityElements;

        //CHANGE TO MAX RELIC VARIABLE


        playerElements?.ForEach(
            (it) =>
            {
                GameObject buttonObj = Instantiate(ElementalBlockUI, listParent.transform);
                ElementalBlockButtonAccesor access = buttonObj.GetComponent<ElementalBlockButtonAccesor>();
                access.Icon.sprite = GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementsSprites[(int)it.BlockElement];
                access.Name.text = it.BlockElement.ToString();

                Button button = buttonObj.GetComponentInChildren<Button>();


                ElementButtonUIList.Add(button);
                elementalBlockUIList.Add(buttonObj);
                   
                
            });

        ElementButtonUIList.Add(AdvanceMenuButton);
        ElementButtonUIList[buttonNavIndex].Select();
    }

    public void SetActiveElement(ElementalBlock element)
    {
        if (GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement.Count < 3) 
        {
            GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement.Add(element);
        }
    }

    public override void OnEnterPanel()
    {
        ElementButtonUIList[buttonNavIndex].Select();

        base.OnEnterPanel();
    }

    public override void GoForward()
    {
        battleManager.SetElements(GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement);
        battleManager.ChangeState(BattleStates.PLAYER_RESOLUTION);

    }

    public override void OnAcceptButton()
    {
        if (buttonNavIndex <= ElementButtonUIList.Count - 2)
        {
            SetActiveElement(playerElements[buttonNavIndex]);
        }
        else if (buttonNavIndex == ElementButtonUIList.Count - 1)
        {
            base.OnAcceptButton();
        }
    }

    public override void OnHoldElementButton()
    {
        base.OnHoldElementButton();
    }

    public override void OnNavigationVertical(int dir)
    {
        buttonNavIndex -= dir;
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, ElementButtonUIList.Count - 1);

        ElementButtonUIList[buttonNavIndex].Select();

    }

    public override void OnNavigationHorizontal(int dir)
    {
        if(dir > 0)
        {
            lastVerticalIndex = buttonNavIndex;
            buttonNavIndex = ElementButtonUIList.Count - 1;
            ElementButtonUIList[buttonNavIndex].Select();
        }
        if(dir < 0)
        {
            buttonNavIndex = lastVerticalIndex;
            ElementButtonUIList[buttonNavIndex].Select();


        }
    }
}
