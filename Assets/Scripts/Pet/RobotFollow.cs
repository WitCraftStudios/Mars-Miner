using UnityEngine;

public class RobotFollow : MonoBehaviour
{
    public Transform player;
    public float followDistance = 3f;
    public float moveSpeed = 3f;
    public Vector3 followOffset = new Vector3(1.5f, 0f, -1.5f); // offset to player's right-rear

    void Update()
    {
        // Calculate the offset relative to player's rotation
        Vector3 targetPosition = player.position
            + player.right * followOffset.x
            + player.forward * followOffset.z;

        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance > 0.1f) // small threshold to avoid jitter
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
