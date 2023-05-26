using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameMenuOptions : UI_ListParent
{
    [SerializeField] Button EquipmentButton;
    [SerializeField] Button ItemsButton;
    [SerializeField] Button ExitButton;

    EventSystem eventSystem;



    public void Init()
    {
        if (eventSystem == null) { eventSystem = EventSystem.current; }
        
    }

    public override void OnEnter()
    {
        EquipmentButton.Select();
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.AddListener(this.HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.AddListener(OnActionButtonPress);
    }
    public override void OnExit()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveListener(OnActionButtonPress);
        currentButtonIndex = 0;
    }
    public override void HandleVerticalArrowMovement(int axis)
    {
        base.HandleVerticalArrowMovement(axis);
        ReselectButton();
    }
    
    protected override void ReselectButton()
    {
        switch (currentButtonIndex)
        {
            case 0:
                eventSystem.SetSelectedGameObject(EquipmentButton.gameObject);
                break;
            case 1:
                eventSystem.SetSelectedGameObject(ItemsButton.gameObject);
                break;
            case 2:
                eventSystem.SetSelectedGameObject(ExitButton.gameObject);
                break; 

        }
    }

    public override void OnActionButtonPress()
    {
        switch (currentButtonIndex)
        {
            case 0:
                New_UI_Manager._UI_MANAGER.GoToEquipmentScreen();
                break;
            case 1:
                //Go to Items Screen
                break;
            case 2:
                Application.Quit();
                //Exit to Main Menu
                break;
        }
    }
}
