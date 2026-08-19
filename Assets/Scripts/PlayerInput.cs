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
        InputSystem.actions.FindAction("Jump").performed += OnJump;
        InputSystem.actions.FindAction("Jump").canceled += OnJump;
        InputSystem.actions.FindAction("Sprint").performed += OnSprint;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();

        if (context.canceled) IsSprinting = false;
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        IsCrouching = !IsCrouching;
        IsSprinting = false;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        IsJumping = context.performed;
        IsCrouching = false;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        if (Move != Vector2.zero)
        {
            IsSprinting = !IsSprinting;
            IsCrouching = false;
        }
    }
}
