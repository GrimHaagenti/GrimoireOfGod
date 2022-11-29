using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementSelection : SinglePanelManager
{

    List<ElementalBlock> playerElements;
    [SerializeField] GameObject elementalBlockUI;
    [SerializeField] GameObject listParent;

    List<GameObject> elementalBlockUIList;

    private void Start()
    {
        //playerElements = GameManager._GAME_MANAGER.player.EntityElements;


        playerElements.ForEach(
            (it) =>
            {
                
                GameObject button = Instantiate(elementalBlockUI, listParent.transform);
                button.GetComponentInChildren<Image>().sprite = it.blockSprite;
                button.GetComponentInChildren<TextMeshProUGUI>().text = it.BlockElement.ToString();
            }
            );
    }




}
