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
    [SerializeField] Animator ResultingElementVisualizerSlot;


    [SerializeField] Image SelectedRune;

    List<ElementalBlock> playerElements;
    Stack<Elements> activeElements = new Stack<Elements>();
    
    List<Button> ElementButtonUIList = new List<Button>();

    ElementalBlockButtonAccesor[] ElementalBlockAccesor;
    int buttonNavIndex = 0;
    List<int> playerElementsQuantities = new List<int>();
    List<string> selectedBlocksAnimationsIndex = new List<string>();
    public override void SetSubmenu()
    {

        battleManager = GameManager._GAME_MANAGER._BATTLE_MANAGER;
        SelectedRune.sprite = battleManager.selectedRune.runeIcon;
        playerElements = battleManager.player._ElementInventory;
        ElementalBlockAccesor = listParent.GetComponentsInChildren<ElementalBlockButtonAccesor>();

        for (int i = 0; i < ElementalBlockAccesor.Length; i++)
        {
            playerElementsQuantities.Add(playerElements.FindAll((it) =>
            {
                if (it.BlockElement == (Elements)i)
                {
                    return true;
                }
                else { return false; }
            }).Count);
            ElementalBlockAccesor[i].Quantity.text = playerElementsQuantities[i].ToString();
            Button button = ElementalBlockAccesor[i].gameObject.GetComponent<Button>();
            ElementButtonUIList.Add(button);
        }

        ElementButtonUIList.Add(AdvanceMenuButton);
        ElementButtonUIList[buttonNavIndex].Select();
    }

    public void SetActiveElement(int elementIndex)
    {
        if (activeElements.Count < 3) 
        {
            activeElements.Push((Elements)elementIndex);
            playerElementsQuantities[elementIndex]--;
            ElementalBlockAccesor[elementIndex].Quantity.text = playerElementsQuantities[elementIndex].ToString();
            string animName = "";
            switch ((Elements)buttonNavIndex)
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
            if (activeElements.Count > 1)
            {
                Elements elem = GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementFusion(activeElements);
                string PreviewAnimName = "";
                switch (elem)
                {
                    case Elements.FIRE:
                        PreviewAnimName = "Fire";
                        break;
                    case Elements.WATER:
                        PreviewAnimName = "Water";
                        break;
                    case Elements.EARTH:
                        PreviewAnimName = "Earth";
                        break;
                    case Elements.WIND:
                        PreviewAnimName = "Wind";
                        break;
                    case Elements.METAL:
                        PreviewAnimName = "Metal";
                        break;
                    case Elements.PLANT:
                        PreviewAnimName = "Plant";
                        break;
                    case Elements.ELECTRICITY:
                        PreviewAnimName = "Electricity";
                        break;
                    case Elements.ICE:
                        PreviewAnimName = "Ice";
                        break;
                    case Elements.ROCK:
                        PreviewAnimName = "Rock";
                        break;
                }
                ResultingElementVisualizerSlot.gameObject.SetActive(true);
                ResultingElementVisualizerSlot.Play(PreviewAnimName);
            }
        }
        
    }

    public void DeselectActiveElement()
    {

        if (activeElements.Count > 0)
        {
            ElementVisualizerSlots[activeElements.Count - 1].gameObject.SetActive(false);

            Elements el = activeElements.Pop();
            playerElementsQuantities[(int)el]++;
            ElementalBlockAccesor[(int)el].Quantity.text = playerElementsQuantities[(int)el].ToString();
            
        }
        if (activeElements.Count > 1)
        {
            Elements elem = GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementFusion(activeElements);
            string PreviewAnimName = "";
            switch (elem)
            {
                case Elements.FIRE:
                    PreviewAnimName = "Fire";
                    break;
                case Elements.WATER:
                    PreviewAnimName = "Water";
                    break;
                case Elements.EARTH:
                    PreviewAnimName = "Earth";
                    break;
                case Elements.WIND:
                    PreviewAnimName = "Wind";
                    break;
                case Elements.METAL:
                    PreviewAnimName = "Metal";
                    break;
                case Elements.PLANT:
                    PreviewAnimName = "Plant";
                    break;
                case Elements.ELECTRICITY:
                    PreviewAnimName = "Electricity";
                    break;
                case Elements.ICE:
                    PreviewAnimName = "Ice";
                    break;
                case Elements.ROCK:
                    PreviewAnimName = "Rock";
                    break;
            }
            ResultingElementVisualizerSlot.gameObject.SetActive(true);
            ResultingElementVisualizerSlot.Play(PreviewAnimName);
        }
        else { 
            ResultingElementVisualizerSlot.gameObject.SetActive(false);
        }
    }


    public override void GoBackButtonPressed()
    {
        if (activeElements.Count > 0) { DeselectActiveElement(); }
        else
        {
            base.GoBackButtonPressed();
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
        
        
        selectedBlocksAnimationsIndex.Clear();

        base.OnExitPanel();
    }

    public override void GoForward()
    {
        List<ElementalBlock> ChosenElements = new List<ElementalBlock>();

        foreach (Elements e in activeElements)
        {
            ElementalBlock newElem = ScriptableObject.CreateInstance<ElementalBlock>();
            newElem.BlockElement = e;
            newElem.Level = ElementLevel.ONE;
            newElem.Potency = 1;
            ChosenElements.Add(newElem);
        }
        battleManager.SetElements(ChosenElements);
        battleManager.ChangeState(BattleStates.PLAYER_RESOLUTION);
        activeElements.Clear();
        selectedBlocksAnimationsIndex.Clear();
        OnExitPanel();
    }

    public override void OnAcceptButton()
    {
        if (buttonNavIndex <= ElementButtonUIList.Count - 2)
        {
            if(playerElementsQuantities[buttonNavIndex] > 0)
            {
                SetActiveElement(buttonNavIndex);
                
            }
            /*
                selectedBlocksIndex.Remove(buttonNavIndex);
                
                DeselectActiveElement(buttonNavIndex);
                ElementButtonUIList[buttonNavIndex].image = buttonAccesors[buttonNavIndex].MainPanel;
                buttonAccesors[buttonNavIndex].MainPanel.gameObject.SetActive(true);
            }*/
        }
        else if (buttonNavIndex == ElementButtonUIList.Count - 1)
        {
            if(activeElements.Count>0)
            {
                base.OnAcceptButton();

            }
        }
    }

    public override void OnHoldElementButton()
    {
        base.OnHoldElementButton();
    }

    public override void OnNavigationVertical(int dir)
    {
        if (dir < 0)
        {
            if(buttonNavIndex <= 5)
            {
                buttonNavIndex += 3;
                ElementButtonUIList[buttonNavIndex].Select();
            }

        }
        if (dir > 0)
        {
            if (buttonNavIndex >= 3)
            {
                buttonNavIndex -= 3;
                ElementButtonUIList[buttonNavIndex].Select();
            }

        }
    }

    public override void OnNavigationHorizontal(int dir)
    {

        buttonNavIndex += dir;
        if (dir > 0)
        {
            if (buttonNavIndex == 3 || buttonNavIndex == 6 || buttonNavIndex == 9)
            {
                buttonNavIndex = ElementButtonUIList.Count - 1;
            }
        }
        buttonNavIndex = Mathf.Clamp(buttonNavIndex, 0, ElementButtonUIList.Count - 1);

        ElementButtonUIList[buttonNavIndex].Select();

    }
}
