using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasBattery = false;

    // Called when player picks up battery
    public void PickupBattery()
    {
        hasBattery = true;
        Debug.Log("Battery picked up!");
    }

    // Called when player delivers battery to rocket
    public void DeliverBattery()
    {
        hasBattery = false;
        Debug.Log("Battery delivered!");
    }
}
