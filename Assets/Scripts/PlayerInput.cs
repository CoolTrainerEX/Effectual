using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; } = Vector2.zero;
    public bool IsCrouching { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;
    public bool IsSprinting { get; private set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem.actions.FindAction("Move").performed += OnMove;
        InputSystem.actions.FindAction("Move").canceled += OnMove;
        InputSystem.actions.FindAction("Crouch").performed += OnCrouch;
        InputSystem.actions.FindAction("Crouch").canceled += OnCrouch;
        InputSystem.actions.FindAction("Jump").performed += OnJump;
        InputSystem.actions.FindAction("Jump").canceled += OnJump;
        InputSystem.actions.FindAction("Sprint").performed += OnSprint;
        InputSystem.actions.FindAction("Sprint").canceled += OnSprint;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        IsCrouching = context.ReadValue<float>() != 0;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        IsJumping = context.ReadValue<float>() != 0;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        IsSprinting = context.ReadValue<float>() != 0;
    }
}
