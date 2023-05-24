using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameMenuContentHandler : MonoBehaviour
{
    [SerializeField] UI_HealthHandler m_UI_PlayerHealth;
    [SerializeField] UI_ElementalContainersHandler m_UI_ElemContainers;
    [SerializeField] UI_EquipmentMainMenuHandler m_UI_EquipmentMainMenu;
    [SerializeField] UI_GameMenuOptions m_UI_MainMenuOptions;


    public void Init()
    {
        m_UI_MainMenuOptions.Init();
    }
    public void EnterMainMenu()
    {
        m_UI_MainMenuOptions.OnEnter();
        UpdateInfo(GameManager._GAME_MANAGER.player);
    }
    public void ExitMainMenu()
    {
        m_UI_MainMenuOptions.OnExit();
    }

    public void UpdateInfo(New_Entity_Script player)
    {
        m_UI_PlayerHealth.UpdatePlayerHealth(player);
        m_UI_ElemContainers.UpdateChargeText(player);
        m_UI_EquipmentMainMenu.UpdatePlayerWeapons(player);
    }
}
