using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Stat Values")]
    public float maxHunger = 100f;
    public float currentHunger;

    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Drain Rates")]
    public float hungerDrainRate = 5f; // per second
    public float healthDrainRate = 10f;

    [Header("UI Sliders")]
    public Slider hungerSlider;
    public Slider healthSlider;

    private void Start()
    {
        currentHunger = maxHunger;
        currentHealth = maxHealth;

        if (hungerSlider != null)
            hungerSlider.maxValue = maxHunger;

        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;
    }

    private void Update()
    {
        DrainHunger();

        if (currentHunger <= 0)
        {
            currentHealth -= healthDrainRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        }

        // Update UI
        if (hungerSlider != null)
            hungerSlider.value = currentHunger;

        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    void DrainHunger()
    {
        currentHunger -= hungerDrainRate * Time.deltaTime;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }

    public void Eat(float foodAmount)
    {
        currentHunger += foodAmount;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
    }
}
