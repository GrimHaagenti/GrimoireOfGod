using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnityEvent_ITEMTYPE : UnityEvent<int>
{

}


public class UI_EquipmentButtonsHandler : UI_ListParent
{
    [SerializeField] UI_WeaponSlotEquipmentButton m_meleeButton;
    [SerializeField] UI_WeaponSlotEquipmentButton m_skill1Button;
    [SerializeField] UI_WeaponSlotEquipmentButton m_skill2Button;
    [SerializeField] UI_WeaponSlotEquipmentButton m_supportButton;
    [SerializeField] Button m_ExitButton;

    public UnityEvent_ITEMTYPE OnWeaponSlotSelected;

    EventSystem eventSystem;
    public void Init()
    {
        if (eventSystem == null) { eventSystem = EventSystem.current; }
        

    }
    public override void OnEnter()
    {
        m_meleeButton.weaponSlotButton.Select();
        OnWeaponSlotSelected = new UnityEvent_ITEMTYPE();

        GoInside();

    }

    public void OnReturn()
    {
        GoInside();
        ReselectButton();
    }

    private void GoInside()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.AddListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.AddListener(OnActionButtonPress);

        New_Entity_Script player = GameManager._GAME_MANAGER.player;

        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.MeleeWeapon), m_meleeButton);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.Skill1Weapon), m_skill1Button);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.Skill2Weapon), m_skill2Button);
        SetWeaponSlotText(player.GetWeaponFromInventoryIndex(player.Equipment.SupportWeapon), m_supportButton);
    }

    public void OnExitToChild()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveListener(OnActionButtonPress);
    }
    public override void OnExit()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveListener (HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveListener(OnActionButtonPress);
        currentButtonIndex = 0;
    }
    public override void HandleVerticalArrowMovement(int axis)
    {
        base.HandleVerticalArrowMovement(axis);
        ReselectButton();
    }

    public override void OnActionButtonPress()
    {
        switch (currentButtonIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                OnWeaponSlotSelected?.Invoke(currentButtonIndex);
                break;
                
                //EXIT
            case 4:
                New_UI_Manager._UI_MANAGER.GoToMainScreen();
                break;
        }
        return;
    }
    protected override void ReselectButton()
    {
        switch (currentButtonIndex)
        {
            case 0:
                m_meleeButton.weaponSlotButton.Select();
                break;
            case 1:
                m_skill1Button.weaponSlotButton.Select();
                break;
            case 2:
                m_skill2Button.weaponSlotButton.Select();
                break;
            case 3:
                m_supportButton.weaponSlotButton.Select();
                break;
            case 4:
                m_ExitButton.Select();
                break;
        }
    }
    private void SetWeaponSlotText(Weapon_Scr? weapon, UI_WeaponSlotEquipmentButton slot)
    {
        if (weapon == null)
        {
            slot.SetName();
        }
        else
        {
            slot.SetName(weapon.WeaponName);
        }
    }
}
