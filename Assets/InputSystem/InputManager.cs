using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager :MonoBehaviour
{
    private PlayerInputs playerInputs;


    //Class Vars
    Scenes InputType;
    public static InputManager _INPUT_MANAGER= null;


    /// 
    /// Exploration Inputs
    
    //Arrow Input for Exploration
    private Vector2 a_exp_navigateInput = Vector2.zero;
    //timers for input actions
    private float t_exp_actionButtonPressed = 0.1f;
    private float t_exp_pauseButtonPressed = 0.1f;


    /// 
    /// Menu Input

    //Arrow Input for Menus
    private Vector2 a_menu_NavigateInput = Vector2.zero;
    //timers for input actions
    private float t_menu_acceptButtonPress = 0.1f;
    private float t_menu_goBackButtonPress = 0.1f;
    private float t_menu_navigatePress = 0.1f;
    private float t_menu_pauseButtonPressed = 0.1f;


    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this);
        }
        else
        {
            _INPUT_MANAGER = this;
        }
        Init();
        DontDestroyOnLoad(this);
    }

    public void Init()
    {
        playerInputs = new PlayerInputs();
        
        // Map Overworld Inputs
        playerInputs.Exploration.Move.performed += exp_NavAxis;
        playerInputs.Exploration.Action.started += exp_ActionButPress;
        playerInputs.Exploration.PauseButton.performed += exp_PauseButPress;

        // Map Menu Inputs
        playerInputs.MenuNavigation.Accept.performed += menu_AcceptButPress;
        playerInputs.MenuNavigation.GoBack.performed += menu_GoBackButPress;
        playerInputs.MenuNavigation.Navigate.performed  += menu_NavAxis;
        playerInputs.MenuNavigation.Navigate.canceled += menu_NavAxisZERO;
        playerInputs.MenuNavigation.PauseButton.performed += menu_PauseButPress;

    }
    private void Update()
    {
        //Exploration Timers
        t_exp_pauseButtonPressed += Time.deltaTime;
        t_exp_actionButtonPressed += Time.deltaTime;

        //Menu Timers
        t_menu_acceptButtonPress += Time.deltaTime;
        t_menu_goBackButtonPress += Time.deltaTime;
        t_menu_navigatePress += Time.deltaTime;
        t_menu_pauseButtonPressed += Time.deltaTime;

        // Update Input System
        InputSystem.Update();
    }

    public Scenes GetInputType()
    {
        return InputType;
    }
    public void SetInputToWorld()
    {
        InputType = Scenes.WORLD;
        playerInputs.MenuNavigation.Disable();
        playerInputs.Exploration.Enable();

        t_menu_acceptButtonPress = 1f;
        t_menu_goBackButtonPress = 1f;
        t_menu_navigatePress = 1f;
        t_menu_pauseButtonPressed = 1f;

        t_exp_actionButtonPressed = 1f;
        t_exp_pauseButtonPressed = 1f;
    }

    public void SetInputToMenu()
    {
        InputType = Scenes.BATTLE;
        playerInputs.Exploration.Disable();
        playerInputs.MenuNavigation.Enable();

        t_menu_acceptButtonPress = 1f;
        t_menu_goBackButtonPress = 1f;
        t_menu_navigatePress = 1f;
        t_menu_pauseButtonPressed = 1f;
        
        a_exp_navigateInput = Vector2.zero;
        t_exp_actionButtonPressed = 1f;
        t_exp_pauseButtonPressed = 1f;
    }


    /// ACCESSORS  /// 

    // Exploration Accessors

    public bool Exploration_GetActionButtonPressed()
    {
        return t_exp_actionButtonPressed == 0f;

    }
    public bool Exploration_GetPauseButtonPressed()
    {
        return t_exp_pauseButtonPressed == 0f;
    }
    public Vector2 Exploration_GetMovementAxis()
    {
        return a_exp_navigateInput;
    }

    // Menu Accessors

    public bool Menu_GetAcceptButtonPressed()
    {
        return t_menu_acceptButtonPress == 0f;
    }
    public bool Menu_GetGoBackButtonPressed()
    {

        return t_menu_goBackButtonPress == 0f;
    }
    /// <summary>
    /// Gets the arrow keys continous input.
    /// </summary>
    /// <returns></returns>
    public Vector2 Menu_GetNavigateInput()
    {
        return a_menu_NavigateInput;
    }
    /// <summary>
    /// Gets the arrow when it's pressed
    /// </summary>
    /// <returns></returns>
    public Vector2 Menu_GetNavigatePressed()
    {
        if (t_menu_navigatePress == 0f)
        {
            return a_menu_NavigateInput.normalized;
        }
        else { return Vector2.zero; }
    }


    public bool Menu_GetPauseButtonPressed()
    {
        return t_menu_pauseButtonPressed == 0f;
    }



    /// INPUT MAPPING (ACCESSORS ABOVE) ///
    //
    // MENUS

    private void menu_NavAxis(InputAction.CallbackContext context)
    {
        t_menu_navigatePress = 0f;
        a_menu_NavigateInput = context.ReadValue<Vector2>();
    }
    private void menu_NavAxisZERO(InputAction.CallbackContext context)
    {
        a_menu_NavigateInput = Vector2.zero;
    }
    private void menu_AcceptButPress(InputAction.CallbackContext context)
    {
        t_menu_acceptButtonPress = 0f; 
    }
    private void menu_GoBackButPress(InputAction.CallbackContext context)
    {
        t_menu_goBackButtonPress = 0f;
    }
    private void menu_PauseButPress(InputAction.CallbackContext context)
    {
        t_menu_pauseButtonPressed = 0f;
    }

    // EXPLORATION

    private void exp_NavAxis(InputAction.CallbackContext context)
    {
        a_exp_navigateInput = context.ReadValue<Vector2>();
    }
    private void exp_ActionButPress(InputAction.CallbackContext context)
    {
        t_exp_actionButtonPressed = 0f;
    }
    private void exp_PauseButPress(InputAction.CallbackContext context)
    {
        t_exp_pauseButtonPressed = 0f;
    }
    //

    
}
