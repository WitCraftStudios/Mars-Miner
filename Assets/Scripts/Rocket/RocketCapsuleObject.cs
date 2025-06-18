using UnityEngine;

public class RocketCapsuleObject : MonoBehaviour
{
    public float riseSpeed = 2f;
    public float fadeDuration = 2f;

    private float fadeTimer;
    private Material objectMaterial;
    private Color startColor;

    void Start()
    {
        fadeTimer = fadeDuration;

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            objectMaterial = renderer.material;
            startColor = objectMaterial.color;
        }
    }

    void Update()
    {
        // Move upward
        transform.Translate(Vector3.up * riseSpeed * Time.deltaTime);

        // Fade out
        if (objectMaterial != null)
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            objectMaterial.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }

        // Destroy after fading
        if (fadeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
