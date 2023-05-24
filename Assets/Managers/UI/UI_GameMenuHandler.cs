using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameMenuHandler : MonoBehaviour
{
    [SerializeField] UI_GameMenuContentHandler m_UI_MainMenuContentHandler;
    [SerializeField] UI_EquipmentMenu m_UI_EquipmentMenu;
    [SerializeField] GameObject m_MainMenuContent;
    [SerializeField] GameObject m_EquipmentMenu;

    public void Init()
    {
        m_UI_MainMenuContentHandler.Init();
        m_UI_EquipmentMenu.Init();
    }

    public void CloseEverything()
    {
        m_UI_MainMenuContentHandler.ExitMainMenu();
        m_UI_EquipmentMenu.ExitEquipmentMenu();

    }
    public void ShowEquipmentScreen()
    {
        m_EquipmentMenu.SetActive(true);
        m_UI_MainMenuContentHandler.ExitMainMenu();
        m_UI_EquipmentMenu.EnterEquipmentMenu();
        m_MainMenuContent.SetActive(false);
    }
    public void ShowMainMenuContent()
    {
        m_MainMenuContent.SetActive(true);
        m_UI_EquipmentMenu.ExitEquipmentMenu();
        m_UI_MainMenuContentHandler.EnterMainMenu();
        m_EquipmentMenu.SetActive(false);
    }
}
