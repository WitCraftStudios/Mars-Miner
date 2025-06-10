using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;
    public Transform cameraTransform; // 👈 Assign your main camera here

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.PlayerMovement.Enable();
        inputActions.PlayerMovement.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        inputActions.PlayerMovement.Jump.performed -= OnJump;
        inputActions.PlayerMovement.Disable();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        moveInput = inputActions.PlayerMovement.Move.ReadValue<Vector2>();

        // 🔁 CAMERA-RELATIVE MOVEMENT
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * moveInput.y + right * moveInput.x;
        controller.Move(move * speed * Time.deltaTime);

        // Gravity and jump
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Optional: Rotate player toward movement direction
        if (move.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
