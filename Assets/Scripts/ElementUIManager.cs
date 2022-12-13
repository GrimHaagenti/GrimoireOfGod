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
    [SerializeField] Animator[] ElementVisualizerSlots;

    int ButtonSizeY = 20;

    List<GameObject> elementalBlockUIList = new List<GameObject>();
    List<ElementalBlock> playerElements;
    List<ElementalBlock> activeElements = new List<ElementalBlock>();
    
    List<Button> ElementButtonUIList = new List<Button>();

    List<ElementalBlockButtonAccesor> buttonAccesors = new List<ElementalBlockButtonAccesor>();

    RectTransform newRectTransform;


    int buttonNavIndex = 0;
    int lastVerticalIndex = 0;
    List<int> selectedBlocksIndex = new List<int>();
    List<string> selectedBlocksAnimationsIndex = new List<string>();
    public override void SetSubmenu()
    {

        newRectTransform = listParent.GetComponent<RectTransform>();
        battleManager = GameManager._GAME_MANAGER._BATTLE_MANAGER;
        playerElements = battleManager.player._ElementInventory;

        //CHANGE TO MAX RELIC VARIABLE


        playerElements?.ForEach(
            (it) =>
            {
                GameObject buttonObj = Instantiate(ElementalBlockUI, listParent.transform);
                ElementalBlockButtonAccesor access = buttonObj.GetComponent<ElementalBlockButtonAccesor>();
                buttonAccesors.Add(access);
                access.Icon.sprite = GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementsSprites[(int)it.BlockElement];
                access.Name.text = it.BlockElement.ToString();

                Button button = buttonObj.GetComponentInChildren<Button>();


                ElementButtonUIList.Add(button);
                elementalBlockUIList.Add(buttonObj);
                   
                
            });

        ElementButtonUIList.Add(AdvanceMenuButton);
        ElementButtonUIList[buttonNavIndex].Select();
    }

    public void SetActiveElement(int elementIndex)
    {
        if (activeElements.Count < 3) 
        {
            activeElements.Add(playerElements[elementIndex]);
            string animName = "";
            switch (playerElements[elementIndex].BlockElement)
            {
                case Elements.FIRE:
                    animName = "Fire";
                    break;
                case Elements.WATER:
                    animName = "Water";
                    break;
                case Elements.EARTH:
                    animName = "Earth";
                    break;
                case Elements.WIND:
                    animName = "Wind";
                    break;
                case Elements.METAL:
                    animName = "Metal";
                    break;
                case Elements.PLANT:
                    animName = "Plant";
                    break;
                case Elements.ELECTRICITY:
                    animName = "Electricity";
                    break;
                case Elements.ICE:
                    animName = "Ice";
                    break;
                case Elements.ROCK:
                    animName = "Rock";
                    break;
            }
            selectedBlocksAnimationsIndex.Add(animName);
            ElementVisualizerSlots[activeElements.Count - 1].gameObject.SetActive(true);
            ElementVisualizerSlots[activeElements.Count - 1].Play(animName);
        }
    }

    public void DeselectActiveElement(int elementIndex)
    {
        foreach (Animator an in ElementVisualizerSlots)
        {
            an.gameObject.SetActive(false);
        }
        int ind = activeElements.IndexOf(playerElements[elementIndex]);
        selectedBlocksAnimationsIndex.RemoveAt(ind);
        activeElements.RemoveAt(ind);
        for (int i = 0; i < activeElements.Count; i++)
        {
            ElementVisualizerSlots[i].gameObject.SetActive(true);
            ElementVisualizerSlots[i].Play(selectedBlocksAnimationsIndex[i]);
        }

    }



    public override void OnEnterPanel()
    {
        base.OnEnterPanel();
        buttonNavIndex = 0;
        ElementButtonUIList[buttonNavIndex].Select();
        activeElements.Clear();
    }
    public override void OnExitPanel()
    {
        buttonNavIndex = 0;
        foreach (Animator a in ElementVisualizerSlots)
        {
            a.gameObject.SetActive(false);
        }
        foreach(ElementalBlockButtonAccesor a in buttonAccesors)
        {
            a.MainPanel.gameObject.SetActive(true);
        }
        
        selectedBlocksIndex.Clear();
        selectedBlocksAnimationsIndex.Clear();

        base.OnExitPanel();
    }

    public override void GoForward()
    {
        battleManager.SetElements(activeElements);
        battleManager.ChangeState(BattleStates.PLAYER_RESOLUTION);
        elementalBlockUIList.ForEach(a =>
        {
            Destroy(a);
        });
        selectedBlocksIndex.Clear();
        elementalBlockUIList.Clear();
        ElementButtonUIList.Clear();
        buttonAccesors.Clear();
        selectedBlocksAnimationsIndex.Clear();
        OnExitPanel();
    }

    public override void OnAcceptButton()
    {
        if (buttonNavIndex <= ElementButtonUIList.Count - 2)
        {
            if (!selectedBlocksIndex.Contains(buttonNavIndex))
            {
                selectedBlocksIndex.Add(buttonNavIndex);
                SetActiveElement(buttonNavIndex);
                buttonAccesors[buttonNavIndex].MainPanel.gameObject.SetActive(false);
                ElementButtonUIList[buttonNavIndex].image = buttonAccesors[buttonNavIndex].BGPanel;

            }
            else
            {
                selectedBlocksIndex.Remove(buttonNavIndex);
                
                DeselectActiveElement(buttonNavIndex);
                ElementButtonUIList[buttonNavIndex].image = buttonAccesors[buttonNavIndex].MainPanel;
                buttonAccesors[buttonNavIndex].MainPanel.gameObject.SetActive(true);
            }
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
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, ElementButtonUIList.Count - 2);

        ElementButtonUIList[buttonNavIndex].Select();
        newRectTransform.offsetMax -= new Vector2(0, dir * ButtonSizeY);




        //if (buttonNavIndex > 3) { newRectTransform.offsetMax += new Vector2(0, dir * ButtonSizeY); }

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
