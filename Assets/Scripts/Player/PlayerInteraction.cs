using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerControls inputActions;
    private IInteractable currentInteractable;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable))
        {
            currentInteractable = interactable;
            Debug.Log("Entered interaction zone: " + other.name);

            InteractionPromptManager.Instance.ShowPrompt("Press [E]");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable) && currentInteractable == interactable)
        {
            currentInteractable = null;
            Debug.Log("Exited interaction zone: " + other.name);

            InteractionPromptManager.Instance.HidePrompt();
        }
    }


    private void OnInteract(InputAction.CallbackContext context)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(gameObject);
            Debug.Log("Interacted with: " + currentInteractable);
        }
        else
        {
            Debug.Log("No interactable in range.");
        }
    }
}
