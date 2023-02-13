using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    PlayerInputActions playerInputActions;

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new();
    }

    void OnEnable()
    {
        playerInputActions.Player.Enable();
    }


    void OnDisable()
    {
        playerInputActions.Player.Disable();
    }


    protected override Vector3 CalculateMoveDirection()
    {
        InputAction moveAction = playerInputActions.Player.Move;
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = Vector3.zero;

        moveDirection += Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized * input.x;
        moveDirection += Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized * input.y;

        return moveDirection;
    }

    protected override Vector3 CalculateRotationDirection()
    {
        if (CalculateMoveDirection() != Vector3.zero)
        {
            Vector3 rotationDirectionFromMovement = CalculateMoveDirection();
            return rotationDirectionFromMovement;
        }
        else
        {
            return transform.forward;
        }
    }
}
