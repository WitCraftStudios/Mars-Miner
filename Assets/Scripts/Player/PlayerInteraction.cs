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
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(gameObject);  // Pass the player GameObject here
            }
        }
    }
}
