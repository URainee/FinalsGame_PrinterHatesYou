using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _nextButton; // deactivated when current step is 3
    [SerializeField] private Button _previousButton; // deactivated when current step is 1
    [SerializeField] private CanvasGroup _nextCanvasGroup;
    [SerializeField] private CanvasGroup _previousCanvasGroup;
    [SerializeField] private Button _exitButton;

    [Header("Steps")]
    [SerializeField] private TextMeshProUGUI _stepText;
    [SerializeField] private int _totalStep;
    [SerializeField] private int _currentStep;

    [Header("Scroll")]
    [SerializeField] private RectTransform[] _stepPanels;
    [SerializeField] private RectTransform _currentPanel;
    [SerializeField] private int _currentPanelCounter;
    [SerializeField] private RectTransform _centerPosition;
    [SerializeField] private RectTransform _westPosition;
    [SerializeField] private RectTransform _eastPosition;

    private Sequence _currentSequence;

    private void Start()
    {
        _currentStep = 1;
        _currentPanelCounter = 0;
        _totalStep = _stepPanels.Length;

        _previousCanvasGroup.interactable = false;

        UpdateText(_currentStep);
        SetupPanels();
    }

    public void NextButton()
    {
        _currentStep++;
        NextPanel();
        UpdateButtons(_currentStep);
        UpdateText(_currentStep);
    }

    public void PreviousButton()
    {
        _currentStep--;
        PreviousPanel();
        UpdateButtons(_currentStep);
        UpdateText(_currentStep);
    }

    private void NextPanel()
    {
        _currentSequence?.Kill();

        _currentSequence = DOTween.Sequence();

        _currentSequence.Append(_stepPanels[_currentPanelCounter].DOMove(_westPosition.position, 0.2f));
        _currentSequence.Join(_stepPanels[_currentStep - 1].DOMove(_centerPosition.position, 0.2f));

        _currentPanelCounter++;
    }

    private void PreviousPanel()
    {
        _currentSequence?.Kill();

        _currentSequence = DOTween.Sequence();

        _currentSequence.Append(_stepPanels[_currentPanelCounter].DOMove(_eastPosition.position, 0.2f));
        _currentSequence.Join(_stepPanels[_currentPanelCounter - 1].DOMove(_centerPosition.position, 0.2f));

        _currentPanelCounter--;

    }

    private void SetupPanels()
    {
        for (int i = 0; i < _stepPanels.Length; i++)
        {
            if (i == 1)
            {
                _stepPanels[0].position = _centerPosition.position;
            }
            
            _stepPanels[i].position = _eastPosition.position;
        }
    }

    private void UpdateButtons(int passStep)
    {
        if (passStep == 1)
        {
            _previousCanvasGroup.interactable = false;
            return;
        }

        if (passStep == _totalStep)
        {
            _nextCanvasGroup.interactable = false;
            return;
        }

        _previousCanvasGroup.interactable = true;
        _nextCanvasGroup.interactable = true;
    }

    private void UpdateText(int passStep)
    {
        _stepText.text = $"{_currentStep}/{_totalStep}";
    }
}
