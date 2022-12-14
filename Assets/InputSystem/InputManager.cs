using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager :MonoBehaviour
{
    private PlayerInputs playerInputs;

    /// <summary>
    /// Exploration Inputs
    /// </summary>
    [SerializeField] public Vector2 moveInput = Vector2.zero;
    [SerializeField] static public float TimeSinceActionButtonPressed = 0.1f;

    public static InputManager _INPUT_MANAGER= null;


    private void Awake()
    {
        if (_INPUT_MANAGER == null)
        {
            _INPUT_MANAGER = new InputManager();
        }
        DontDestroyOnLoad(this);
    }
    /// <summary>
    /// MenuNavigation
    /// </summary>
    static public float TimeSinceAcceptButtonPressed = 0.1f;

    static public float TimeSinceGoBackButtonPressed = 0.1f;

    
    public Vector2 NavigateInput = Vector2.zero;
    static public float TimeSinceNavigatePressed = 0.1f;


    static public float TimeSinceHoldElementButtonPressed = 0.1f;
    
    public float TimeSincePauseButtonPressed = 0.1f;
    public float MenuTimeSincePauseButtonPressed = 0.1f;

    Scenes InputType;

    public void Init()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Exploration.Enable();

        playerInputs.Exploration.Move.performed += MoveInput;
        playerInputs.Exploration.Action.started += ActionInput;
        playerInputs.Exploration.PauseButton.performed += PauseInput;


        playerInputs.MenuNavigation.Accept.performed += AcceptButtonInput;
        playerInputs.MenuNavigation.GoBack.performed += GoBackButtonInput;
        playerInputs.MenuNavigation.Navigate.started     += NavigateAxis;
        playerInputs.MenuNavigation.HoldElement.performed += HoldElementButtonInput;
        playerInputs.MenuNavigation.PauseButton.performed += MenuPauseInput;

    }
    private void Update()
    {
        TimeSinceActionButtonPressed += Time.deltaTime;
        TimeSinceAcceptButtonPressed += Time.deltaTime;
        TimeSinceGoBackButtonPressed += Time.deltaTime;
        TimeSinceHoldElementButtonPressed += Time.deltaTime;
        TimeSinceNavigatePressed += Time.deltaTime;
        TimeSincePauseButtonPressed += Time.deltaTime;
        MenuTimeSincePauseButtonPressed += Time.deltaTime;

        InputSystem.Update();

    }

    private void HoldElementButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceHoldElementButtonPressed = 0f;
    }
    private void NavigateAxis(InputAction.CallbackContext context)
    {
        TimeSinceNavigatePressed = 0f;
        NavigateInput = context.ReadValue<Vector2>();


    }
    private void GoBackButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceGoBackButtonPressed = 0f;
    }
    private void AcceptButtonInput(InputAction.CallbackContext context)
    {
        TimeSinceAcceptButtonPressed = 0f; 
    }
    private void PauseInput(InputAction.CallbackContext context)
    {
        TimeSincePauseButtonPressed = 0f;
    }
    private void MenuPauseInput(InputAction.CallbackContext context)
    {
        MenuTimeSincePauseButtonPressed = 0f;
    }
    public Scenes GetInputType()
    {
        return InputType;
    }
    public void ChangeInputType(Scenes type)
    {
        if (type == Scenes.BATTLE)
        {
            InputType = Scenes.BATTLE;
            moveInput = Vector2.zero;
            playerInputs.Exploration.Disable();
            playerInputs.MenuNavigation.Enable();
        }

        if(type == Scenes.WORLD)
        {
            InputType = Scenes.WORLD;
            playerInputs.MenuNavigation.Disable();
            playerInputs.Exploration.Enable();
        }
        TimeSinceActionButtonPressed =1f;
        TimeSinceAcceptButtonPressed = 1f;
        TimeSinceGoBackButtonPressed = 1f;
        TimeSinceHoldElementButtonPressed = 1f;
        TimeSinceNavigatePressed = 1f;
        TimeSincePauseButtonPressed = 1f;
        MenuTimeSincePauseButtonPressed = 1f;
    }

    

    private void MoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void ActionInput(InputAction.CallbackContext context)
    {
        TimeSinceActionButtonPressed = 0f;
    }



    public bool IsActionButtonPressed()
    {
        return TimeSinceActionButtonPressed == 0f;
      
    }
    public bool IsAcceptButtonPressed()
    {
        return TimeSinceAcceptButtonPressed == 0f;
    }
    public bool IsGoBackButtonPressed()
    {

        return TimeSinceGoBackButtonPressed == 0f;
    } 
    public bool IsHoldElementsButtonPressed()
    {

        return TimeSinceHoldElementButtonPressed == 0f;
      
    }
    public Vector2 IsNavigateInput()
    {
        if (TimeSinceNavigatePressed == 0f)
        {
            Debug.Log("AA");
            return NavigateInput.normalized;
        }
        else { return Vector2.zero; }
    }

    public bool IsPauseButtonPressed()
    {
        return TimeSincePauseButtonPressed == 0f;
    }
    public bool IsMenuPauseButtonPressed()
    {
        return MenuTimeSincePauseButtonPressed == 0f;
    }
}
