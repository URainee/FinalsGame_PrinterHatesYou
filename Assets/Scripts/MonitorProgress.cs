using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MonitorProgress : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private float animationDuration = 1f;

    private void Start()
    {
        UpdateProgressValue(1f); // Animate from the current fill to full
    }

    public void UpdateProgressValue(float targetValue)
    {
        if (fillImage == null)
        {
            Debug.LogError("Assign a Fill Image in the Inspector.", this);
            return;
        }

        targetValue = Mathf.Clamp01(targetValue);

        fillImage.DOKill();
        fillImage.DOFillAmount(targetValue, animationDuration);
    }
}
