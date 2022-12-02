using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUIManager : MonoBehaviour
{
    [SerializeField] BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    [SerializeField] protected List<Button> optionButtons;

    [SerializeField] protected Button AdvanceMenuButton;


    [SerializeField] GameObject ElementalBlockUI;
    [SerializeField] GameObject listParent;


    List<GameObject> elementalBlockUIList = new List<GameObject>();
    List<ElementalBlock> playerElements;
    List<ElementalBlock> activeElements = new List<ElementalBlock>();


    public void SetSubmenu(List<ElementalBlock> elementalBlocks)
    {
        playerElements = elementalBlocks;
        //CHANGE TO MAX RELIC VARIABLE

        playerElements?.ForEach(
            (it) =>
            {

                if(it.Quantities.Length > 3)
                {
                    Debug.LogError("Too many level slots");
                    return;
                }
                for (int i = 0; i < it.Quantities.Length; i++)
                {
                    if (it.Quantities[i] > 0)
                    {
                        GameObject button = Instantiate(ElementalBlockUI, listParent.transform);
                        ElementalBlockButtonAccesor access = button.GetComponent<ElementalBlockButtonAccesor>();
                        access.Icon.sprite = it.blockSprite;
                        access.Name.text = it.BlockElement.ToString();
                        access.Number.text = "x" + it.Quantities[i];

                        button.GetComponentInChildren<Button>().onClick.AddListener(() =>
                        {
                            SetActiveElement(it,i);
                        });

                        elementalBlockUIList.Add(button);
                    }
                }
            }
            );
    }

    public void SetActiveElement(ElementalBlock element, int LevelIndex)
    {
        if (battleManager.selectedRune.relicsElement[(int)element.BlockElement].Quantities[LevelIndex] < 3)
        {
            battleManager.selectedRune.relicsElement[(int)element.BlockElement].Quantities[LevelIndex]++;
        }
    }

    void Start()
    {
        AdvanceMenuButton.onClick.AddListener(() => {
            battleManager.SetElements(activeElements);
            battleManager.ChangeState(BattleStates.PLAYER_RESOLUTION);
            gameObject.SetActive(false);
        });
    }
}
