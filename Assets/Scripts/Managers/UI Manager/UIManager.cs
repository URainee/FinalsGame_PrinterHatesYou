using System;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private static int TestPass;

    //*
    //Events and Actions
    //*
    public event Action<int> OnPassTest;

    public event Action<int, int> OnPrintProgress;

    public event Action OnPrint;
    public event Action OnPrinterJam;
    public event Action OnPrinterUnjam;
    public event Action OnPrinterDisconnected;
    public event Action OnPrinterConnected;

    //*
    //MonoBehavior Functions
    //*

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    //*
    //Logic (Events)
    //*

    public void PassTest(int _passedTest)
    {
        if (OnPassTest != null)
        {
            OnPassTest(_passedTest);
        }
    }

    public void PrintProgress(int _passProgress, int _passMaxProgress)
    {
        if (OnPrintProgress != null)
        {
            OnPrintProgress(_passProgress, _passMaxProgress);
        }
    }

    public void Print()
    {
        if (OnPrint != null)
        {
            OnPrint();
        }
    }

    public void PrinterJam()
    {
        if (OnPrinterJam != null)
        {
            OnPrinterJam();
        }
    }

    public void PrinterUnjam()
    {
        if (OnPrinterUnjam != null)
        {
            OnPrinterUnjam();
        }
    }

    public void PrinterDisconnected()
    {
        if (OnPrinterDisconnected != null)
        {
            OnPrinterDisconnected();
        }
    }

    public void PrinterConnected()
    {
        if (OnPrinterConnected != null)
        {
            OnPrinterConnected();
        }
    }
}
