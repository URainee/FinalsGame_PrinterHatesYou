using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //*
    // As of November 14, 2026...it is UNFINISHED
    //*

    public static GameManager Instance { get; private set; }

    private enum Endings
    {
        GoodEnding, // Printed in time
        BadEnding, // Did not print in time
        BrokeEnding // Broke the printer...
    }

    private bool _hasEnding;

    private int _score;
    [SerializeField] private int _targetScore;

    [Header("Time")]
    [SerializeField] private int _startMinutes;
    public int StartMinutes { get { return _startMinutes; } }


    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _hasEnding = false;

        _score = 0;
    }

    public void AddScore()
    {
        //*
        //Placed in Tray
        //*

        if (_score < _targetScore)
        {
            _score++;
        }

        if(_score == _targetScore)
        {
            PlayEnding(1);
        }
    }

    public void PlayEnding(int choice)
    {
        //_hasEnding exists to make it so that it only selects one ending and has no chance of two endings happening at the same
        //time. Can be deleted if it causes issues in the future.

        if(_hasEnding == false)
        {
            switch (choice)
            {
                case 1:
                    Debug.Log("Good Ending");
                    _hasEnding = true;
                    break;
                case 2:
                    Debug.Log("Bad Ending");
                    _hasEnding = true;
                    break;
                case 3:
                    // Made a reference to it in the PrinterScript
                    Debug.Log("Broke Ending");
                    _hasEnding = true;
                    break;
                default:
                    Debug.Log("No Such Ending Exists");
                    break;
            }
        }
            
    }
}
