using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum Directions_Enum { UP, DOWN, LEFT, RIGHT, NO_DIR }
public class UI_PlayerActionButtonHandler : MonoBehaviour
{
    [SerializeField] Button MeleeButton;
    [SerializeField] Button Skill1Button;
    [SerializeField] Button Skill2Button;
    [SerializeField] Button SupportButton;
    TextMeshProUGUI MeleeButtonText;
    TextMeshProUGUI Skill1ButtonText;
    TextMeshProUGUI Skill2ButtonText;
    TextMeshProUGUI SupportButtonText;

    EventSystem _eventSystem;

    private Button currentButton= null;
    private Directions_Enum currentDir = Directions_Enum.NO_DIR;

    private void Awake()
    {
        _eventSystem = EventSystem.current;
        MeleeButtonText = MeleeButton.GetComponentInChildren<TextMeshProUGUI>();
        Skill1ButtonText = Skill1Button.GetComponentInChildren<TextMeshProUGUI>();
        Skill2ButtonText = Skill2Button.GetComponentInChildren<TextMeshProUGUI>();
        SupportButtonText = SupportButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ShowButtons(bool hide)
    {
        MeleeButton.gameObject.SetActive(hide);
        Skill1Button.gameObject.SetActive(hide);
        Skill2Button.gameObject.SetActive(hide);
        SupportButton.gameObject.SetActive(hide);
    }
    
    public void InitButtons(New_Entity_Script player)
    {
        Entity_Equipment playerEquip = player.Equipment;

        if(playerEquip.MeleeWeapon == -1)
        {
            MeleeButtonText.text = "";
            MeleeButton.interactable = false;
        }
        else
        {
            MeleeButtonText.text = player.GetWeaponFromInventoryIndex(playerEquip.MeleeWeapon).WeaponName;
        }

        if (playerEquip.Skill1Weapon == -1)
        {
            Skill1ButtonText.text = "";
            Skill1Button.interactable = false;
        }
        else
        {
            Skill1ButtonText.text = player.GetWeaponFromInventoryIndex(playerEquip.Skill1Weapon).WeaponName;
        }

        if (playerEquip.Skill2Weapon == -1)
        {
            Skill2ButtonText.text = "";
            Skill2Button.interactable = false;
        }
        else
        {
            Skill2ButtonText.text = player.GetWeaponFromInventoryIndex(playerEquip.Skill2Weapon).WeaponName;
        }

        if (playerEquip.SupportWeapon == -1)
        {
            SupportButtonText.text = "";
            SupportButton.interactable = false;
        }
        else
        {
            SupportButtonText.text = player.GetWeaponFromInventoryIndex(playerEquip.SupportWeapon).WeaponName;
        }
    }
    
    public void HighlightButton(Directions_Enum dir)
    {
        switch (dir)
        {
            case Directions_Enum.UP:
                currentButton = SupportButton;
                break;
            case Directions_Enum.DOWN:
                currentButton = MeleeButton;
                break;
            case Directions_Enum.LEFT:
                currentButton = Skill1Button;
                break;
            case Directions_Enum.RIGHT:
                currentButton = Skill2Button;
                break;
            case Directions_Enum.NO_DIR:
                _eventSystem.SetSelectedGameObject(null);
                currentButton= null;
                break;
        }

        currentDir = dir;
        currentButton?.Select();
    }


}