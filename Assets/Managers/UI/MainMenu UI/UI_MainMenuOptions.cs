using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenuOptions : UI_ListParent
{
    [SerializeField] Button PlayGameButton;
    [SerializeField] Button ExitButton;


    public override void OnActionButtonPress()
    {
        switch (currentButtonIndex)
        {
            case 0:// PLAY GAME
                GameManager._GAME_MANAGER.LoadGame();
                break;
            case 1: // APP EXIT
                Application.Quit();
                break;
        }
    }


    public override void HandleVerticalArrowMovement(int axis)
    {
        base.HandleVerticalArrowMovement(axis);
        ReselectButton();
    }
    public override void OnEnter()
    {
        currentButtonIndex= 0;
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.AddListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.AddListener(OnActionButtonPress);


        ReselectButton();
    }

    public override void OnExit()
    {
        New_UI_Manager._UI_MANAGER.OnVerticalAxis?.RemoveListener(HandleVerticalArrowMovement);
        New_UI_Manager._UI_MANAGER.OnActionButtonPressed?.RemoveListener(OnActionButtonPress);
    }

    protected override void ReselectButton()
    {
        switch (currentButtonIndex)
        {
            case 0:// PLAY GAME
                PlayGameButton.Select();
                break;
            case 1: // APP EXIT
                ExitButton.Select();
                break;
        }
    }
}
