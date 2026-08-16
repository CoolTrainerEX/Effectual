using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Vector3 Motion { get; private set; } = Vector3.zero;
    public bool IsCrouching { get; private set; } = false;
    public bool IsJumping { get; private set; } = false;

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float crouchMultiplier = 0.5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float crouchSpeed = 2;
    [SerializeField] private float rotationSpeed = 2;
    [SerializeField] private float yGroundVelocity = -1;

    private CharacterController controller;
    private PlayerInput input;
    private float controllerBaseRadius;
    private float controllerBaseHeight;
    private float yVelocity = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        controllerBaseRadius = controller.radius;
        controllerBaseHeight = controller.height;
    }

    // Update is called once per frame
    void Update()
    {
        Motion = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * new Vector3(input.Move.x, 0, input.Move.y).normalized;
        IsCrouching = input.IsCrouching;
        IsJumping = input.IsJumping && controller.isGrounded;

        if (IsJumping) yVelocity = Mathf.Sqrt(jumpHeight * -2.0f * Physics.gravity.y);
        else if (controller.isGrounded)
        {
            float controllerRadius = controllerBaseRadius;
            float controllerHeight = controllerBaseHeight;

            if (IsCrouching)
            {
                Motion *= crouchMultiplier;
                controllerRadius *= 2;
                controllerHeight *= 0.75f;
            }
            else if (input.IsSprinting) Motion *= sprintMultiplier;

            controller.radius = Mathf.MoveTowards(controller.radius, controllerRadius, crouchSpeed * Time.deltaTime);
            controller.height = Mathf.MoveTowards(controller.height, controllerHeight, crouchSpeed * Time.deltaTime);
            controller.center = new Vector3(controller.center.x, controller.height / 2, controller.center.z);
            yVelocity = yGroundVelocity;
        }
        else yVelocity += Physics.gravity.y * Time.deltaTime;

        if (Motion != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Motion), rotationSpeed * Time.deltaTime);

        Motion = Motion * moveSpeed + yVelocity * Vector3.up;

        controller.Move(Motion * Time.deltaTime);
    }
}
