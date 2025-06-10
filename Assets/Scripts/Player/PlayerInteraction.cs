using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 3f;
    public LayerMask interactLayer;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.PlayerMovement.Enable();
        inputActions.PlayerMovement.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.PlayerMovement.Interact.performed -= OnInteract;
        inputActions.PlayerMovement.Disable();
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        Vector3 origin = transform.position + Vector3.up; // slightly above ground
        Vector3 direction = transform.forward;

        float radius = 0.5f; // adjust as needed

        Debug.DrawRay(origin, direction * interactRange, Color.red, 2f);

        if (Physics.SphereCast(origin, radius, direction, out RaycastHit hit, interactRange, interactLayer))
        {
            Debug.Log("Hit object: " + hit.collider.name);

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log("Interacting with: " + hit.collider.name);
                interactable.Interact(gameObject);
            }
        }
        else
        {
            Debug.Log("No interactable hit.");
        }
    }




}
