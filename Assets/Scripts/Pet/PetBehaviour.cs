using UnityEngine;
using UnityEngine.InputSystem;

public class PetBehavior : MonoBehaviour, IInteractable
{
    public GameObject mapPanel;
    public string interactionPrompt = "Press [E] to interact with Pet";
    public Transform lookAtTarget;
    public GameObject playerModel; // Assign the visible player object

    private CameraFollow cameraFollow;
    private PlayerInput playerInput;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void Interact(GameObject interactor)
    {
        if (mapPanel != null)
        {
            mapPanel.SetActive(true);
        }

        if (cameraFollow != null && lookAtTarget != null)
        {
            cameraFollow.LockCameraTo(lookAtTarget);
        }

        if (playerInput != null)
        {
            playerInput.enabled = false; // disables movement/input
        }

        if (playerModel != null)
        {
            playerModel.SetActive(false); // hides player mesh
        }
    }

    public string GetInteractionPrompt() => interactionPrompt;

    public void ExitInteraction()
    {
        if (mapPanel != null)
        {
            mapPanel.SetActive(false);
        }

        if (cameraFollow != null)
        {
            cameraFollow.UnlockCamera();
        }

        if (playerInput != null)
        {
            playerInput.enabled = true;
        }

        if (playerModel != null)
        {
            playerModel.SetActive(true);
        }
    }
}
