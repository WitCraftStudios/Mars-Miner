using UnityEngine;

public class RocketCapsule : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Battery sent to Earth!");
        // TODO: Add rocket animation and reward logic
    }
}
