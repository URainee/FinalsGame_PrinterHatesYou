using UnityEngine;
using UnityEngine.UI;

public class PlayerPunchPrinter : MonoBehaviour
{
    [SerializeField] private float coolDown = 0.1f;
    [SerializeField] private int damagePerHit = 2;

    private float nextClick;

    private PrinterHealth currentHealth;

    private void Awake()
    {
        currentHealth = GetComponent<PrinterHealth>();
    }
    private void OnMouseDown()
    {
        if (Time.time < nextClick)
        {
            return;
        }

        nextClick = Time.time + coolDown;
        currentHealth.TakeDamage(damagePerHit);
    }
}
