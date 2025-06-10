using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 2f;
    public float rotationSpeed = 1.5f;
    public float minY = -20f;
    public float maxY = 60f;

    private float yaw = 0f;
    private float pitch = 10f;
    private Vector2 lookInput;

    private PlayerControls input;

    void Awake()
    {
        input = new PlayerControls();
        input.PlayerMovement.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.PlayerMovement.Look.canceled += _ => lookInput = Vector2.zero;
    }

    void OnEnable()
    {
        input.PlayerMovement.Enable();
    }

    void OnDisable()
    {
        input.PlayerMovement.Disable();
    }

    void LateUpdate()
    {
        if (target == null) return;

        yaw += lookInput.x * rotationSpeed;
        pitch -= lookInput.y * rotationSpeed;
        pitch = Mathf.Clamp(pitch, minY, maxY);

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -distance);
        Vector3 desiredPosition = target.position + Vector3.up * height + offset;

        transform.position = desiredPosition;
        transform.LookAt(target.position + Vector3.up * height);
    }
}
