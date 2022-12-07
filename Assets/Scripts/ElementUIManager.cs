using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementUIManager : MonoBehaviour
{
    BattleManager battleManager;

    [SerializeField] protected GameObject NextPanel;
    [SerializeField] protected List<Button> optionButtons;

    [SerializeField] protected Button AdvanceMenuButton;


    [SerializeField] GameObject ElementalBlockUI;
    [SerializeField] GameObject listParent;


    List<GameObject> elementalBlockUIList = new List<GameObject>();
    List<ElementalBlock> playerElements;
    List<ElementalBlock> activeElements = new List<ElementalBlock>();


    public void SetSubmenu(List<ElementalBlock> elementalBlocks, BattleManager bM)
    {
        playerElements = elementalBlocks;
        //CHANGE TO MAX RELIC VARIABLE

        battleManager = bM;

        playerElements?.ForEach(
            (it) =>
            {
                GameObject button = Instantiate(ElementalBlockUI, listParent.transform);
                ElementalBlockButtonAccesor access = button.GetComponent<ElementalBlockButtonAccesor>();
                access.Icon.sprite = GameManager._GAME_MANAGER._ELEMENT_FACTORY.ElementsSprites[(int)it.BlockElement];
                access.Name.text = it.BlockElement.ToString();

                button.GetComponentInChildren<Button>().onClick.AddListener(() =>
                {
                    SetActiveElement(it);
                });

                elementalBlockUIList.Add(button);
                   
                
            }
            );
    }

    public void SetActiveElement(ElementalBlock element)
    {
        if (GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement.Count < 3) 
        {
            GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement.Add(element);
        }
    }

    void Start()
    {
        AdvanceMenuButton.onClick.AddListener(() => {
            if (GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement.Count > 0)
            {
                battleManager.SetElements(GameManager._GAME_MANAGER._BATTLE_MANAGER.selectedRune.relicsElement);
                battleManager.ChangeState(BattleStates.PLAYER_RESOLUTION);
                gameObject.SetActive(false);
            }
        });
    }
}
