using UnityEngine;

public class RocketCapsule : MonoBehaviour, IInteractable
{
    public int coinReward = 10;
    public GameManager gameManager; // reference to GameManager
    public GameObject rocketLaunchEffect; // VFX prefab

    public void Interact(GameObject interactor)
    {
        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null && inventory.hasBattery)
        {
            LaunchRocket(inventory);
        }
        else
        {
            Debug.Log("You need to bring a battery to launch the rocket.");
        }
    }

    void LaunchRocket(PlayerInventory inventory)
    {
        Debug.Log("LaunchRocket() called");

        Debug.Log("Rocket launching!");


        inventory.DeliverBattery(); // Remove battery from player

        if (gameManager != null)
        {
            gameManager.AddCoins(coinReward);
        }

        if (rocketLaunchEffect != null)
        {
            Debug.Log("Instantiating launch effect");
            Instantiate(rocketLaunchEffect, transform.position, Quaternion.identity);
        }
    }

}
