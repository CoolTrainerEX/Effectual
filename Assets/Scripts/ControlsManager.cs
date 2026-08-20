using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsManager : MonoBehaviour
{
    public bool IsTouchscreen { get; private set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        InputSystem.onActionChange += OnActionChange;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed) IsTouchscreen = ((InputAction)obj).activeControl.device is Touchscreen;
    }
}
