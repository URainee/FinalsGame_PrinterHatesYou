using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button printButton;

    private void Start()
    {
        printButton.onClick.AddListener(PrintButtonAnimation);
    }
    void PrintButtonAnimation()
    {
        printButton.transform.DOScale(1.2f, 0.2f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => printButton.transform.DOScale(1f, 0.2f));
    }
}
