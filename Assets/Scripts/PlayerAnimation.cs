using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimation : MonoBehaviour
{
    private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int IsCrouchingHash = Animator.StringToHash("IsCrouching");
    private static readonly int MotionZHash = Animator.StringToHash("MotionZ");
    private static readonly int MotionXHash = Animator.StringToHash("MotionX");

    [SerializeField, Min(0)] private float animationDampTime = 0.05f;

    private Animator animator;
    private CharacterController controller;
    private PlayerMovement movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var localMotion = transform.InverseTransformDirection(movement.Motion);

        if (movement.IsJumping) animator.SetTrigger(JumpHash);

        animator.SetFloat(MotionXHash, localMotion.x, animationDampTime, Time.deltaTime);
        animator.SetFloat(MotionZHash, localMotion.z, animationDampTime, Time.deltaTime);
        animator.SetBool(IsCrouchingHash, movement.IsCrouching);
        animator.SetBool(IsFallingHash, !controller.isGrounded && movement.Motion.y < 0);
    }
}
