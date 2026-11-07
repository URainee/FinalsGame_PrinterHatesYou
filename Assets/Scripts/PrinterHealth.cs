using UnityEngine;
using UnityEngine.UI;

public class PrinterHealth : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private Slider healthbar;
    [SerializeField] private int maxHealth = 100;

    [Header("Regeneration")]
    [SerializeField] private int regenPerSecond = 1;
    [SerializeField] private float delay = 1f;
    [SerializeField] private float interval = 1f;

    private int health;

    private float lastDamagePoint;
    private float nextRegenPoint;

    private bool broken;


    private void Start()
    {
        health = maxHealth;

        healthbar.minValue = 0;
        healthbar.maxValue = maxHealth;

        UpdateMeter();

        nextRegenPoint = Time.time + interval;
    }


    public void TakeDamage(int amount)
    {
        if (broken)
        {
            return;
        }

        health = Mathf.Max(0, health - amount);
        lastDamagePoint = Time.time;

        UpdateMeter();

        if (health <= 0)
        {
            BreakingPoint();
        }
    }


    private void Update()
    {
        if (broken || health >= maxHealth)
        {
            return;
        }

        if (Time.time < lastDamagePoint + delay)
        {
            return;
        }

        if (Time.time < nextRegenPoint)
        {
            return;
        }

        RegenHealth();
    }


    private void RegenHealth()
    {
        health = Mathf.Min(maxHealth, health + regenPerSecond);

        UpdateMeter();

        nextRegenPoint = Time.time + interval;
    }


    private void UpdateMeter()
    {
        healthbar.value = health;
    }


    private void BreakingPoint()
    {
        broken = true;
        Debug.Log("Printer broke!");
    }
}
