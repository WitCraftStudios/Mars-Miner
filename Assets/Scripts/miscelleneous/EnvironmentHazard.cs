using UnityEngine;

public class EnvironmentHazard : MonoBehaviour
{
    public Driller driller;
    public float timeBetweenHits = 20f;
    public float damageAmount = 30f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenHits)
        {
            driller.TakeDamage(damageAmount);
            Debug.Log("Driller hit by asteroid/sandstorm!");
            timer = 0f;
        }
    }
}
