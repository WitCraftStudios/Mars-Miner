using UnityEngine;

public class Driller : MonoBehaviour, IInteractable
{
    RocketCapsule capsule;
    public bool isRunning = false;
    public float batteryProductionTime = 10f;
    public float batteryTimer;

    [Header("Battery")]
    public GameObject batteryObject; // scene object (battery)
    public Transform batterySpawnPoint; // empty transform in scene

    [Header("Damage System")]
    public bool isDamaged = false;
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        batteryTimer = batteryProductionTime;

        // Hide battery at start
        if (batteryObject != null)
        {
            batteryObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isRunning && !isDamaged)
        {
            batteryTimer -= Time.deltaTime;

            if (batteryTimer <= 0f)
            {
                ProduceBattery();
                batteryTimer = batteryProductionTime;
            }
        }
    }

    public void Interact(GameObject interactor)
    {
        if (isDamaged)
        {
            Repair();
            return;
        }

        isRunning = !isRunning;
        Debug.Log("Driller toggled: " + (isRunning ? "ON" : "OFF"));
    }

    public string GetInteractionPrompt()
    {
        return "Press [E]";  // or whatever prompt text you want
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Driller took damage: " + damage);
        if (currentHealth <= 0)
        {
            isDamaged = true;
            isRunning = false;
            Debug.Log("Driller is damaged and stopped.");
        }
    }

    void Repair()
    {
        currentHealth = maxHealth;
        isDamaged = false;
        Debug.Log("Driller repaired.");
    }

    void ProduceBattery()
    {
        if (batteryObject != null && batterySpawnPoint != null)
        {
            batteryObject.transform.position = batterySpawnPoint.position;
            batteryObject.transform.rotation = batterySpawnPoint.rotation;
            batteryObject.SetActive(true);

            Debug.Log("Battery is now visible at spawn point.");
        }
    }

    public void BatteryCollected()
    {
        if (batteryObject != null)
        {
            batteryObject.SetActive(false);
            batteryTimer = batteryProductionTime; // restart timer
            Debug.Log("Battery collected. Driller will produce a new one after delay.");
        }
    }



}
