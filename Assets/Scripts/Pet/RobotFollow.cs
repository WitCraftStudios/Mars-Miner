using UnityEngine;
using UnityEngine.AI;

public class RobotFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 3f;
    public float alertHungerThreshold = 20f;

    public GameObject hungerIconObject; // Assign the UI icon GameObject here

    private NavMeshAgent agent;
    private PlayerStats playerStats;

    private bool hasAlertedLowHunger = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            Debug.LogError("Player not assigned to PetBehavior.");
            enabled = false;
            return;
        }

        playerStats = player.GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats component not found on player.");
            enabled = false;
            return;
        }

        if (hungerIconObject != null)
        {
            hungerIconObject.SetActive(false); // Hide at start
        }
    }

    void Update()
    {
        FollowPlayer();
        CheckHungerAlert();
    }

    void FollowPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath();
        }

        FacePlayer();
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    void CheckHungerAlert()
    {
        if (playerStats.currentHunger <= alertHungerThreshold && !hasAlertedLowHunger)
        {
            hasAlertedLowHunger = true;
            ShowHungerIcon(true);
        }
        else if (playerStats.currentHunger > alertHungerThreshold && hasAlertedLowHunger)
        {
            hasAlertedLowHunger = false;
            ShowHungerIcon(false);
        }
    }

    void ShowHungerIcon(bool show)
    {
        if (hungerIconObject != null)
        {
            hungerIconObject.SetActive(show);
        }
    }
}
