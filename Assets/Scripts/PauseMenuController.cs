using DG.Tweening;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public RectTransform[] pauseMenu;
    public float slideDuration = 0.6f;
    public float delayBetweenPauseMenu = 0.2f;
    public float startX = -1020f;

    private Sequence currentTween;

    private Vector2[] targetPositions;

    private void Awake()
    {
        // Resets the scene on load
        Time.timeScale = 1.0f;

        targetPositions = new Vector2[pauseMenu.Length];

        for (int i = 0; i < pauseMenu.Length; i++)
        {
            if (pauseMenu[i] == null) continue;

            // The Pause Menu's Inspector position should be its visible destination.
            targetPositions[i] = pauseMenu[i].anchoredPosition;

            // Hide it offscreen at the start.
            pauseMenu[i].anchoredPosition =
                new Vector2(startX, targetPositions[i].y);
        }
    }

    public void PauseGame()
    {

        Time.timeScale = 0f;

        for (int i = 0; i < pauseMenu.Length; i++)
        {
            if (pauseMenu[i] == null) continue;

            pauseMenu[i].DOAnchorPos(targetPositions[i], slideDuration)
                        .SetEase(Ease.OutBack)
                        .SetDelay(i * delayBetweenPauseMenu)
                        .SetUpdate(true);
        }
    }

    public void ResumeGame()
    {
        currentTween?.Kill();

        currentTween = DOTween.Sequence().SetUpdate(true);

        // Sequence sequence = DOTween.Sequence().SetUpdate(true);

        for (int i = 0; i < pauseMenu.Length; i++)
        {
            if (pauseMenu[i] == null) continue;

            pauseMenu[i].DOKill();

            Vector2 offscreenPosition = new Vector2(startX, targetPositions[i].y);

            currentTween.Insert(i * delayBetweenPauseMenu, pauseMenu[i].DOAnchorPos(offscreenPosition, slideDuration).SetEase(Ease.InBack));

            /*
            sequence.Insert(
                i * delayBetweenPauseMenu,
                pauseMenu[i].DOAnchorPos(offscreenPosition, slideDuration)
                    .SetEase(Ease.InBack)
            ); */
        }

        currentTween.OnComplete(() => Time.timeScale = 1f);

        //clears the currentTween reference
        currentTween.OnKill(() => currentTween = null);

        //sequence.OnComplete(() => Time.timeScale = 1f);
    }
}
