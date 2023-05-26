using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EquipmentMenu : MonoBehaviour
{
    [SerializeField] UI_EquipmentButtonsHandler m_UI_equipmentButtonHandler;
    [SerializeField] UI_EquipmentItemsListHandler m_UI_EquipmentListHandler;
    public void Init()
    {
        m_UI_equipmentButtonHandler.Init();
    }

    public void EnterEquipmentMenu()
    {
        m_UI_equipmentButtonHandler.OnEnter();

        m_UI_equipmentButtonHandler.OnWeaponSlotSelected?.AddListener(GoToWeaponSelectionMenu);
        m_UI_EquipmentListHandler.ReturnToEquipment?.AddListener(ReturnToEquipmentMenu);
    }
    public void ReturnToEquipmentMenu()
    {
        m_UI_equipmentButtonHandler.OnReturn();
    }


    public void GoToWeaponSelectionMenu(int slot)
    {
        m_UI_equipmentButtonHandler.OnExitToChild();
        m_UI_EquipmentListHandler.SetTypeAndEnter(slot);
    }

    public void ExitEquipmentMenu()
    {
        m_UI_equipmentButtonHandler.OnExit();
        m_UI_equipmentButtonHandler.OnWeaponSlotSelected?.RemoveListener(GoToWeaponSelectionMenu);
        m_UI_EquipmentListHandler.ReturnToEquipment?.RemoveListener(ReturnToEquipmentMenu);
    }

}
