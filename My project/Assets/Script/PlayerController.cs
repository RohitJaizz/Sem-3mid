using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementWithAnimations : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float acceleration = 10f;
    public float rotationSpeed = 10f;

    [Header("References")]
    public Transform cameraTransform;
    public Animator animator;

    private CharacterController controller;
    private Vector3 moveDirection;
    private float currentSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (animator == null) animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleAnimation();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        // Calculate move direction relative to camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 targetDirection = (forward * vertical + right * horizontal).normalized;

        // Choose target speed
        float targetSpeed = (isRunning ? runSpeed : walkSpeed) * targetDirection.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * acceleration);

        moveDirection = targetDirection * currentSpeed;

        // Move player
        controller.SimpleMove(moveDirection);

        // Rotate player smoothly toward movement direction
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void HandleAnimation()
    {
        float animSpeed = currentSpeed / runSpeed; // normalized 0–1
        animator.SetFloat("Speed", animSpeed);
    }
}
