using System.Collections;
using UnityEngine;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public TextMeshProUGUI text; 
    public Color color1 = Color.green; 
    public Color color2 = Color.red; 
    public float flashDuration = 0.5f; 

    void Start()
    {
        StartCoroutine(FlashText());
    }

    private IEnumerator FlashText()
    {
        while (true) // Loop indefinitely
        {
            text.color = color1; // Set to first color
            yield return new WaitForSeconds(flashDuration); // Wait
            text.color = color2; // Set to second color
            yield return new WaitForSeconds(flashDuration); // Wait
        }
    }
}
