using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interactor);
    string GetInteractionPrompt(); // Add this to provide custom prompt text
}
