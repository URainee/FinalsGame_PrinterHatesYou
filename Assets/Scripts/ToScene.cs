using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScene : MonoBehaviour
{
    [SerializeField] private RectTransform transitionPanel;
    [SerializeField] private string sceneToLoad;

    public void TranitionToScene()
    {
        transitionPanel.anchoredPosition = new Vector2(-Screen.width, 0);

        transitionPanel.DOAnchorPos(Vector2.zero, 0.5f)
                       .SetEase(Ease.OutCubic)
                       .OnComplete(() => SceneManager.LoadScene(sceneToLoad));
    }
}
