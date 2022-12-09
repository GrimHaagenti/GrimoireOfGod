using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputManager 
{
    private PlayerInputs playerInputs;

    /// <summary>
    /// Exploration Inputs
    /// </summary>
    [SerializeField] public Vector2 moveInput = Vector2.zero;
    [SerializeField] public bool ActionButtonPressed = false;
    [SerializeField] static public float TimeSinceActionButtonPressed = 0.1f;

    /// <summary>
    /// MenuNavigation
    /// </summary>
    public bool AcceptButtonPressed = false;
    static public float TimeSinceAcceptButtonPressed = 0.1f;

    public bool GoBackButtonPressed = false;
    static public float TimeSinceGoBackButtonPressed = 0.1f;

    
    public Vector2 NavigateInput = Vector2.zero;
    static public float TimeSinceNavigatePressed = 0.1f;



    public bool HoldElementButtonPressed = false;
    static public float TimeSinceHoldElementButtonPressed = 0.1f;



    public void Init()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Exploration.Enable();

        playerInputs.Exploration.Move.performed += MoveInput;
        playerInputs.Exploration.Action.performed += ActionInput;
        playerInputs.Exploration.PauseButton.performed += PauseInput;


        playerInputs.MenuNavigation.Accept.performed += AcceptButtonInput;
        playerInputs.MenuNavigation.GoBack.performed += GoBackButtonInput;
        playerInputs.MenuNavigation.Navigate.performed += NavigateAxis;
        playerInputs.MenuNavigation.HoldElement.performed += HoldElementButtonInput;

    }
    public void Update()
    {

        if (TimeSinceActionButtonPressed > 0)
        {
            ActionButtonPressed = false;
        }
        if (TimeSinceAcceptButtonPressed > 0)
        {
            AcceptButtonPressed = false;
        }
        if (TimeSinceGoBackButtonPressed > 0)
        {
            GoBackButtonPressed = false;
        }
        if (TimeSinceHoldElementButtonPressed > 0)
        {
            HoldElementButtonPressed = false;
        }

        if (TimeSinceNavigatePressed > 0)
        {
            NavigateInput = Vector2.zero;
        }

        TimeSinceActionButtonPressed += Time.deltaTime;
        TimeSinceAcceptButtonPressed += Time.deltaTime;
        TimeSinceGoBackButtonPressed += Time.deltaTime;
        TimeSinceHoldElementButtonPressed += Time.deltaTime;
        TimeSinceNavigatePressed += Time.deltaTime;

        InputSystem.Update();

    }

    private void HoldElementButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceHoldElementButtonPressed = 0f;
        HoldElementButtonPressed = true;
    }
    private void NavigateAxis(InputAction.CallbackContext context)
    {
        TimeSinceNavigatePressed = 0f;
        NavigateInput = context.ReadValue<Vector2>();


    }
    private void GoBackButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceGoBackButtonPressed = 0f;
        GoBackButtonPressed = true;
    }
    private void AcceptButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceAcceptButtonPressed = 0f; 
        AcceptButtonPressed = true;
    }
    private void PauseInput(InputAction.CallbackContext context)
    {

    }

    public void ChangeInputType(Scenes type)
    {
        if (type == Scenes.BATTLE)
        {
            moveInput = Vector2.zero;
            playerInputs.Exploration.Disable();
            playerInputs.MenuNavigation.Enable();
        }

        if(type == Scenes.WORLD)
        {
            playerInputs.MenuNavigation.Disable();
            playerInputs.Exploration.Enable();
        }
    }

    

    private void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void ActionInput(InputAction.CallbackContext context)
    {
        TimeSinceActionButtonPressed = 0f;
        ActionButtonPressed = true;
    }

    public bool IsActionButtonPressed()
    {
        return TimeSinceActionButtonPressed == 0f;
      
    }
    public bool IsAcceptButtonPressed()
    {
        Debug.Log("AAA");
        return TimeSinceAcceptButtonPressed == 0f;
    }
    public bool IsGoBackButtonPressed()
    {
        Debug.Log("AAA");

        return TimeSinceGoBackButtonPressed == 0f;
    } 
    public bool IsHoldElementsButtonPressed()
    {
        Debug.Log("AAA");

        return TimeSinceHoldElementButtonPressed == 0f;
      
    }
}
