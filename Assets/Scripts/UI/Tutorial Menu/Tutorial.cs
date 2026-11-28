using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    [SerializeField] private RectTransform _centerPosition;
    [SerializeField] private RectTransform _westPosition;
    [SerializeField] private RectTransform _eastPosition;

    private Sequence _currentSequence;

    private void Start()
    {
        SetupPanels();

        _totalStep = _stepPanels.Length;
        _currentStep = 1;

        _stepText.text = $"{_currentStep}/{_totalStep}";

        _previousCanvasGroup.interactable = false;
    }

    public void NextButton()
    {
        if (_currentStep < _totalStep)
        {
            _currentStep++;
            NextPanel();
        }
    }

    public void PreviousButton()
    {
        PreviousPanel();
    }

    private void NextPanel()
    {
        _currentSequence?.Kill();

        _currentSequence = DOTween.Sequence();

        _currentSequence.Append(_currentPanel.DOMove(_westPosition.position, 0.2f));
        _currentSequence.Join(_stepPanels[_currentStep - 1].DOMove(_centerPosition.position, 0.2f));

        if (_currentStep == _totalStep)
        {
            _nextButton.interactable = false;
        }
    }

    private void PreviousPanel()
    {

    }

    private void SetupPanels()
    {
        for (int i = 0; i < _stepPanels.Length; i++)
        {
            if (i == 1)
            {
                _stepPanels[0].position = _centerPosition.position;
                _stepPanels[0] = _currentPanel;
            }
            
            _stepPanels[i].position = _eastPosition.position;
        }
    }

    private void UpdateButtons()
    {

    }
}
