using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 Motion { get; private set; } = Vector3.zero;
    public bool IsCrouching { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;

    [SerializeField] private PlayerSettings settings;

    private CharacterController controller;
    private PlayerInput input;
    private float controllerBaseRadius;
    private float controllerBaseHeight;
    private float yVelocity = 0;
    private bool isSprinting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        controllerBaseRadius = controller.radius;
        controllerBaseHeight = controller.height;
    }

    // Update is called once per frame
    private void Update()
    {
        Motion = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(input.Move.x, 0, input.Move.y);
        IsCrouching = input.IsCrouching;
        IsJumping = input.IsJumping && controller.isGrounded;

        if (controller.isGrounded) isSprinting = input.IsSprinting;

        if (IsJumping) yVelocity = Mathf.Sqrt(settings.jumpHeight * -2.0f * Physics.gravity.y);
        else if (controller.isGrounded)
        {
            var controllerRadius = controllerBaseRadius;
            var controllerHeight = controllerBaseHeight;

            if (IsCrouching)
            {
                Motion *= settings.crouchMultiplier;
                controllerRadius *= 2;
                controllerHeight *= 0.75f;
            }

            controller.radius = Mathf.MoveTowards(controller.radius, controllerRadius, settings.crouchSpeed * Time.deltaTime);
            controller.height = Mathf.MoveTowards(controller.height, controllerHeight, settings.crouchSpeed * Time.deltaTime);
            controller.center = new Vector3(controller.center.x, controller.height / 2, controller.center.z);
            yVelocity = settings.yGroundVelocity;
        }
        else yVelocity += Physics.gravity.y * Time.deltaTime;

        if (isSprinting) Motion *= settings.sprintMultiplier;
        if (Motion != Vector3.zero) transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0), settings.rotationSpeed * Time.deltaTime);

        Motion = Motion * settings.moveSpeed + yVelocity * Vector3.up;

        controller.Move(Motion * Time.deltaTime);
    }
}
