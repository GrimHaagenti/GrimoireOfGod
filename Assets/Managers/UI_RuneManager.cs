using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RuneManager : SinglePanelManager
{




    public delegate void RuneClicked(Sprite sprite);

    public event RuneClicked OnRuneClicked;

    private void Awake()
    {
        OnRuneClicked = SetActiveRune;
        optionButtons.ForEach((it) =>
        {
            Sprite spr = it.gameObject.GetComponentInChildren<Image>().sprite; 

            it.onClick.AddListener(()=> OnRuneClicked(spr));

        });

        AdvanceMenuButton?.onClick.AddListener(() => parentMenu.GoToNextSubMenu()); 
    }
   
    public void SetActiveRune(Sprite runeSprite)
    {
        AdvanceMenuButton.GetComponentInChildren<Image>().sprite = runeSprite; 
    
    }

}
