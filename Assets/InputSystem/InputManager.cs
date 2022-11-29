using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class InputManager 
{
    private PlayerInputs playerInputs;

    [SerializeField] public Vector2 moveInput = Vector2.zero;

    [SerializeField] public bool ActionButtonPressed = false;
    [SerializeField] public float TimeSinceActionButtonPressed = 0.1f;


    public void Init()
    {
        playerInputs = new PlayerInputs();
        playerInputs.Exploration.Enable();

        playerInputs.Exploration.Move.performed += MoveInput;
        playerInputs.Exploration.Action.performed += ActionInput;

    }
    public void Update()
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
