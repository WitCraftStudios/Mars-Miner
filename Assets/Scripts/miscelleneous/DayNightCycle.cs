using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light sunLight;
    public float dayDurationInMinutes = 1f; // 1-minute day
    private float timeOfDay = 0f;

    void Update()
    {
        float daySpeed = 360f / (dayDurationInMinutes * 60f);
        timeOfDay += Time.deltaTime * daySpeed;
        timeOfDay %= 360f;

        sunLight.transform.rotation = Quaternion.Euler(timeOfDay, -30f, 0f);

        // Optional: change intensity based on time
        float dot = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Clamp01(dot);
    }
}
