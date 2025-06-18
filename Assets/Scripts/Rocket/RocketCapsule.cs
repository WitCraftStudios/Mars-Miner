using UnityEngine;

public class RocketCapsule : MonoBehaviour, IInteractable
{
    public int coinReward = 10;
    public GameManager gameManager;

    public float launchSpeed = 5f;
    public float launchDuration = 3f;
    public float launchDelay = 2f;

    public GameObject rocketPrefab; // Assign your rocket prefab here

    private bool isWaitingToLaunch = false;
    private bool isLaunching = false;
    private float launchTimer = 0f;
    private float delayTimer = 0f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        // Save the starting position and rotation to respawn rocket later
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void Interact(GameObject interactor)
    {
        if (isWaitingToLaunch || isLaunching)
        {
            Debug.Log("Rocket is already preparing or launching.");
            return;
        }

        PlayerInventory inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null && inventory.hasBattery)
        {
            inventory.DeliverBattery();
            StartLaunchDelay();
        }
        else
        {
            Debug.Log("You need to bring a battery to launch the rocket.");
        }
    }

    public string GetInteractionPrompt()
    {
        return "Press [E] to launch rocket";  // or whatever prompt text you want
    }
    private void StartLaunchDelay()
    {
        isWaitingToLaunch = true;
        delayTimer = 0f;
        Debug.Log("Launch sequence started. Waiting to launch...");
    }

    private void Update()
    {
        if (isWaitingToLaunch)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer >= launchDelay)
            {
                isWaitingToLaunch = false;
                StartLaunch();
            }
        }

        if (isLaunching)
        {
            transform.Translate(Vector3.up * launchSpeed * Time.deltaTime);

            launchTimer += Time.deltaTime;
            if (launchTimer >= launchDuration)
            {
                isLaunching = false;
                Debug.Log("Rocket launch finished.");

                if (gameManager != null)
                {
                    gameManager.AddCoins(coinReward);
                }

                RespawnRocket();
            }
        }
    }

    private void StartLaunch()
    {
        isLaunching = true;
        launchTimer = 0f;
        Debug.Log("Rocket is launching now!");
    }

    private void RespawnRocket()
    {
        GameObject newRocket = Instantiate(rocketPrefab, originalPosition, originalRotation);

        RocketCapsule newRocketCapsule = newRocket.GetComponent<RocketCapsule>();
        if (newRocketCapsule != null)
        {
            newRocketCapsule.rocketPrefab = this.rocketPrefab;
            newRocketCapsule.gameManager = this.gameManager; // carry over reference
        }

        Destroy(gameObject); // Destroy current rocket
    }

}
