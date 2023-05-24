using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/Actions/Player Move Action")]
public class PlayerMoveAction : Action
{
    float accel = 0;
    float turnToCameraAngles = 35f;
    Vector2 lastInput = Vector2.zero;
    [SerializeField] AnimationClip IdleAnimation;
    public override void Act(StateController controller)
    {
        PlayerMove(controller);
    }

    private void PlayerMove(StateController controller)
    {
        Vector2 input = InputManager._INPUT_MANAGER.Exploration_GetMovementAxis();

        if (!controller.worldStats.ActionInterrumptMovement)
        {
            float targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(controller.gameObject.transform.eulerAngles.y, targetRotation, ref controller.worldStats.turnSmoothSpeed, controller.worldStats.turnSmoothTime);

            Vector3 velocity = Vector3.zero;

            if (input.magnitude != 0)
            {
                accel += controller.worldStats.Acceleration * Time.deltaTime;
                accel = Mathf.Min(controller.worldStats.MaxWalkSpeed, accel);
                velocity.x = input.x * accel;
                velocity.z = input.y * accel;
                lastInput = input;
                controller.gameObject.transform.rotation = Quaternion.Euler(0, angle, 0f);
                controller.PlayAnimation(EntityStates.MOVE);
            }
            else
            {
                accel -= controller.worldStats.Decceleration * Time.deltaTime;
                accel = Mathf.Max(0, accel);
                velocity.x = lastInput.x * accel;
                velocity.z = lastInput.y * accel;
                controller.PlayAnimation(EntityStates.IDLE);
            }


            controller.character.Move(velocity * Time.deltaTime);

        }
    }
}
