using UnityEngine;

public class Driller : MonoBehaviour, IInteractable
{
    public bool isRunning = false;

    public void Interact()
    {
        isRunning = !isRunning;
        Debug.Log("Driller toggled: " + (isRunning ? "ON" : "OFF"));
        // TODO: Add particle effect or animation
    }
}
