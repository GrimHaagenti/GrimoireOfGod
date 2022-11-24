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

                GameObject button = Instantiate(ElementalBlockUI, listParent.transform);
                button.GetComponentInChildren<Image>().sprite = it.blockSprite;
                button.GetComponentInChildren<Button>().onClick.AddListener(() => 
                { SetActiveRune(it);
                });

                elementalBlockUIList.Add(button);
            }
            );
    }

    public void SetActiveRune(ElementalBlock element)
    {
        activeElements.Add(element);
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
