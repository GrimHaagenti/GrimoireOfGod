using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_RuneManager : MonoBehaviour
{
    [SerializeField] List<Button> runesButtons;

    [SerializeField] Button selectedRuneButton;

    public delegate void RuneClicked(Sprite sprite);

    public event RuneClicked OnRuneClicked;

    private void Awake()
    {
        OnRuneClicked = SetActiveRune;
        runesButtons.ForEach((it) =>
        {
            Sprite spr = it.gameObject.GetComponentInChildren<Image>().sprite; 

            it.onClick.AddListener(()=> OnRuneClicked(spr));

        });
    }

    public void SetActiveRune(Sprite runeSprite)
    {
        selectedRuneButton.GetComponentInChildren<Image>().sprite = runeSprite; 
    
    }

}
