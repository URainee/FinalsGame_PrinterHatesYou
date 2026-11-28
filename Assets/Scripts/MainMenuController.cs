using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private RectTransform transitionPanel;

    public void Play()
    {
        SceneManager.LoadScene("DialogueScreen");
    }

    public void Options()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void Credits()
    {
        transitionPanel.anchoredPosition = new Vector2(Screen.width, 0);

        transitionPanel.DOAnchorPos(Vector2.zero, 0.5f)
                       .SetEase(Ease.OutCubic)
                       .OnComplete(() => SceneManager.LoadScene("CreditsMenu"));
    }

    public void Quit()
    {
        Application.Quit();
    }
}
