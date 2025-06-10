using UnityEngine;

public class SandstormController : MonoBehaviour
{
    public ParticleSystem sandstormParticles;
    public float stormDuration = 15f;
    public float stormInterval = 60f;

    private float timer;
    private bool isStorming = false;

    void Start()
    {
        timer = stormInterval;
        if (sandstormParticles.isPlaying)
            sandstormParticles.Stop();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (!isStorming && timer <= 0)
        {
            StartCoroutine(StartStorm());
            timer = stormInterval; // reset for next storm
        }
    }

    System.Collections.IEnumerator StartStorm()
    {
        Debug.Log("Sandstorm starting!");
        isStorming = true;
        sandstormParticles.Play();

        yield return new WaitForSeconds(stormDuration);

        sandstormParticles.Stop();
        isStorming = false;
        Debug.Log("Sandstorm ended.");
    }

    public bool IsStorming()
    {
        return isStorming;
    }
}
