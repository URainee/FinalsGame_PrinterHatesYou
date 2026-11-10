using UnityEngine;
using UnityEngine.UI;

public class PlayerPunchPrinter : MonoBehaviour
{
    [SerializeField] private float coolDown = 0.1f;
    [SerializeField] private int damagePerHit = 2;

    private float nextClick;

    private PrinterHealth currentHealth;

    // for the other script
    private PrinterScript _printerScript;

    private void Awake()
    {
        // currentHealth = GetComponent<PrinterHealth>();

        _printerScript = GetComponent<PrinterScript>();
    }
    private void OnMouseDown()
    {
        if (Time.time < nextClick)
        {
            return;
        }

        nextClick = Time.time + coolDown;
        // currentHealth.TakeDamage(damagePerHit);
        _printerScript.TakeDamage(damagePerHit);
    }
}
