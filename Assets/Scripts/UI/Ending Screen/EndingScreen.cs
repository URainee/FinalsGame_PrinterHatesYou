using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _endingCanvasGroup;
    [SerializeField] private TextMeshProUGUI _endingText; //Phrase related to ending you got...
    [SerializeField] private Button _endingButton; //Button to load the dialogue scene...

    private void Start()
    {
        UIManager.Instance.OnGameEnd += GameEnd;

        _endingCanvasGroup.alpha = 0f;
        _endingCanvasGroup.interactable = false;
        _endingCanvasGroup.blocksRaycasts = false;
    }

    private void GameEnd(int passEndID)
    {
        Debug.Log("End Test Three");

        _endingCanvasGroup.alpha = 1f;
        _endingCanvasGroup.interactable = true;
        _endingCanvasGroup.blocksRaycasts = true;

        switch (passEndID)
        {
            case 1:
                //Referenced here in the GameManager
                _endingText.text = "All materials have been printed!";
                break;
            case 2:
                //Referenced in TimerScript
                _endingText.text = "You didn't make it in time...";
                break;
            case 3:
                //Referenced in PrinterScript's DestroyPrinter()
                _endingText.text = "You broke the printer...";
                break;
            default:
                break;
        }
    }
}
