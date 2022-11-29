using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInputs;
    public static InputManager _INPUT_MANAGER;

    public Vector2 moveInput = Vector2.zero;

    public bool ActionButtonPressed = false;
    public float TimeSinceActionButtonPressed = 0.1f;

    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(_INPUT_MANAGER);
        }
        else
        {

            playerInputs = new PlayerInputs();
            playerInputs.Exploration.Enable();

            playerInputs.Exploration.Move.performed += MoveInput;
            playerInputs.Exploration.Action.performed += ActionInput;
            _INPUT_MANAGER = this;

        }
    }
    private void Update()
    {
        TimeSinceActionButtonPressed += Time.deltaTime;

        if (TimeSinceActionButtonPressed > 0.1f)
        {
            ActionButtonPressed = false;
        }
        
     
        InputSystem.Update();
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
}
