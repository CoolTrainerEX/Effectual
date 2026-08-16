using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsCrouchingHash = Animator.StringToHash("IsCrouching");
    private static readonly int MotionZHash = Animator.StringToHash("MotionZ");
    private static readonly int MotionXHash = Animator.StringToHash("MotionX");

    [SerializeField] private float animationDampTime = 0.05f;

    private CharacterController controller;
    private Animator animator;
    private PlayerMovement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localMotion = transform.InverseTransformDirection(movement.Motion);

        if (movement.IsJumping) animator.SetTrigger(JumpHash);

        animator.SetFloat(MotionXHash, localMotion.x, animationDampTime, Time.deltaTime);
        animator.SetFloat(MotionZHash, localMotion.z, animationDampTime, Time.deltaTime);
        animator.SetBool(IsCrouchingHash, movement.IsCrouching);
        animator.SetBool(IsFallingHash, !controller.isGrounded && movement.Motion.y < 0);
    }
}
