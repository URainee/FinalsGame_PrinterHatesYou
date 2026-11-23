using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{
    //*
    //Variables
    //*

    [SerializeField] private GameObject _connectionIcon;
    [SerializeField] private Sprite _connectedIconImage;
    [SerializeField] private Sprite _disconnectedIconImage;

    [SerializeField] private TextMeshProUGUI _printerDebugText;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private Image fillImage;
    [SerializeField] private float animationDuration = 1f;

    //*
    //MonoBehavior Functions
    //*

    private void Start()
    {
        UIManager.Instance.OnPrint += OnPrint;
        UIManager.Instance.OnPrinterJam += OnPrinterJam;
        UIManager.Instance.OnPrinterUnjam += OnPrinterUnjam;
        UIManager.Instance.OnPrinterConnected += OnPrinterConnected;
        UIManager.Instance.OnPrinterDisconnected += OnPrinterDisconnected;

        UIManager.Instance.OnPrintProgress += UpdateProgressValue;

        PrinterDebugStatus(0);

        UIManager.Instance.OnPassTest += MonitorTest;
    }

    //*
    //Logic
    //*

    #region Monitor

    private void MonitorTest(int passedNumber)
    {
        Debug.Log($"The passed number is {passedNumber}");
    }

    private void UpdateProgressValue (int passProgress, int passMaxProgress)
    {
        if (fillImage == null)
        {
            Debug.LogError("Assign a Fill Image in the Inspector.", this);
            return;
        }

        float progress = (float)passProgress;

        float maxProgress = (float)passMaxProgress;

        progress = Mathf.Clamp01(progress / maxProgress);

        float progressPercentage = Mathf.Round(progress * 100);

        fillImage.DOKill();

        fillImage.DOFillAmount(progress, animationDuration);

        _progressText.text = $"{progressPercentage.ToString()}%";
    }

    #endregion

    #region Printer

    private void OnPrint()
    {
        PrinterDebugStatus(1);
    }

    private void OnPrinterJam()
    {
        PrinterDebugStatus(2);
        // Change icons to red
    }

    private void OnPrinterUnjam()
    {
        PrinterDebugStatus(3);
        // Change icons to green
    }

    private void OnPrinterConnected()
    {
        SwitchConnectionIcon(true);
        PrinterDebugStatus(5);
        // Change wifi icon
    }

    private void OnPrinterDisconnected()
    {
        SwitchConnectionIcon(false);
        PrinterDebugStatus(4);
        // Change wifi icon
    }

    private void PrinterDebugStatus(int passID)
    {
        switch (passID)
        {
            case 0:
                _printerDebugText.text = "Printer Is Idle";
                break;
            case 1:
                _printerDebugText.text = "Printing...";
                break;
            case 2:
                _printerDebugText.text = "Error : Printer Jammed";
                break;
            case 3:
                _printerDebugText.text = "Printer Unjammed";
                break;
            case 4:
                _printerDebugText.text = "Error : Printer Disconnected";
                break;
            case 5:
                _printerDebugText.text = "Printer Connected";
                break;
            case 6:
                _printerDebugText.text = "Error : Printer Unresponsive";
                break;
            default:
                _printerDebugText.text = "If you see me...run";
                break;
        }
    }

    #endregion

    #region Connection
    private void SwitchConnectionIcon(bool isConnected)
    {
        Image image = _connectionIcon.GetComponent<Image>();

        if (isConnected)
        {
            image.sprite = _connectedIconImage;
        }
        else
        {
            image.sprite = _disconnectedIconImage;
        }
    }

    #endregion
}
