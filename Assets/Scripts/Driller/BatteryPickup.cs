using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Battery triggered by: {other.name}");
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null && !inventory.hasBattery)
            {
                gameObject.SetActive(false);
                inventory.PickupBattery();
                
            }
        }
    }

}
