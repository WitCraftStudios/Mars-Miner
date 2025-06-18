using UnityEngine;

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
    private Transform lockTarget = null;

    [Header("Lock Settings")]
    public float lockedDistance = 2f;
    public float lockedHeight = 1f;
    public bool isLocked = false;

    void Awake()
    {
        input = new PlayerControls();
        input.PlayerMovement.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.PlayerMovement.Look.canceled += _ => lookInput = Vector2.zero;
    }

    void OnEnable() => input.PlayerMovement.Enable();
    void OnDisable() => input.PlayerMovement.Disable();

    void LateUpdate()
    {
        if (target == null) return;

        Transform actualTarget = isLocked && lockTarget != null ? lockTarget : target;

        if (!isLocked) // normal rotation
        {
            yaw += lookInput.x * rotationSpeed;
            pitch -= lookInput.y * rotationSpeed;
            pitch = Mathf.Clamp(pitch, minY, maxY);
        }

        float currentDistance = isLocked ? lockedDistance : distance;
        float currentHeight = isLocked ? lockedHeight : height;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 offset = rotation * new Vector3(0, 0, -currentDistance);
        Vector3 desiredPosition = actualTarget.position + Vector3.up * currentHeight + offset;

        transform.position = desiredPosition;
        transform.LookAt(actualTarget.position + Vector3.up * currentHeight);
    }

    public void LockCameraTo(Transform lockTransform)
    {
        lockTarget = lockTransform;
        isLocked = true;
    }

    public void UnlockCamera()
    {
        isLocked = false;
        lockTarget = null;
    }

}
