using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_GameMenuParentHandler : UI_ParentStandard
{
    [SerializeField] UI_FadePanel m_fadePanel;
    [SerializeField] UI_GameMenuHandler m_MainMenuHandler;

    bool isMenuShow = false;
    bool isMenuAnimPlaying = false;

    ///EVENTS
    
    
    public void Init()
    {
        New_UI_Manager._UI_MANAGER.OnMenuButtonPressed?.AddListener(HandleShowMenu);

        m_fadePanel = New_UI_Manager._UI_MANAGER.fadePanel;
        m_fadePanel.OnFadeInComplete?.AddListener(ShowMainMenuScreen);
        m_fadePanel.OnFadeOutComplete?.AddListener(CloseMenu);
        m_MainMenuHandler.Init();

        ShowPanel(false);
    }

    public void DONTCALLENTERNOW()
    {
        m_fadePanel.OnFadeInComplete?.RemoveListener(ShowMainMenuScreen);

    }
    public void CanCallNow()
    {
        //m_fadePanel.OnFadeInComplete?.AddListener(ShowMainMenuScreen);

    }

   
    void HandleShowMenu()
    {
        if (!isMenuAnimPlaying)
        {
            isMenuAnimPlaying = true;
            if (isMenuShow)
            {
                HidePanel_Menu();
            }
            else
            {
                ShowPanel_Menu();
            }
        }
    }

    public void ShowPanel_Menu()
    {
        m_fadePanel.BeginFadeIn();
    }
    public void HidePanel_Menu()
    {
        ShowPanel(false);
        m_fadePanel.BeginFadeOut();
    }

    public void ShowEquipmentScreen()
    {
        m_MainMenuHandler.ShowEquipmentScreen();
    }
    public void ShowMainMenuScreen()
    {
        isMenuShow = true;
        ShowPanel(true);
        InputManager._INPUT_MANAGER.SetInputToMenu();
        m_MainMenuHandler.ShowMainMenuContent();
        isMenuAnimPlaying = false;
    }
    public void CloseMenu()
    {
        isMenuAnimPlaying = false;
        isMenuShow = false;
        m_MainMenuHandler.CloseEverything();
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveAllListeners();
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveAllListeners();
        InputManager._INPUT_MANAGER.SetInputToWorld();

    }

}
