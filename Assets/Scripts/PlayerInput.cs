using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; } = Vector2.zero;
    public bool IsCrouching { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;
    public bool IsSprinting { get; private set; } = false;

    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        crouchAction = InputSystem.actions.FindAction("Crouch");
        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        crouchAction.performed += OnCrouch;
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJump;
        sprintAction.performed += OnSprint;
    }

    void OnDestroy()
    {
        moveAction.performed -= OnMove;
        moveAction.canceled -= OnMove;
        crouchAction.performed -= OnCrouch;
        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJump;
        sprintAction.performed -= OnSprint;
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
