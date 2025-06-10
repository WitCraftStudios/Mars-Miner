using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class CameraInput : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;

    private PlayerControls inputActions;
    private Vector2 lookInput;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.PlayerMovement.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerMovement.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.PlayerMovement.Look.performed -= ctx => lookInput = ctx.ReadValue<Vector2>();
        inputActions.PlayerMovement.Look.canceled -= ctx => lookInput = Vector2.zero;
        inputActions.Disable();
    }

    private void Update()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.m_XAxis.Value += lookInput.x * Time.deltaTime * 200f;
            freeLookCamera.m_YAxis.Value += lookInput.y * Time.deltaTime * 0.01f;
        }
    }
}
