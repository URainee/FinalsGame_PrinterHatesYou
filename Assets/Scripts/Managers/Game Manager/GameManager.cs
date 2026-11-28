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

        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Time.timeScale = 1.0f;

        // Makes sure that if the game comes from a scene where
        // timescale is set 0, then it opens to a timescale
        // set to one

        _hasEnding = false;

        _score = 0;
    }

    private void Update()
    {
        Debug.Log(Time.timeScale);
    }

    public void AddScore(int passedScore)
    {
        //*
        //Referenced in Tray
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
                    //Referenced here in the GameManager
                    Debug.Log("Good Ending");
                    _hasEnding = true;
                    break;
                case 2:
                    //Referenced in TimerScript
                    Debug.Log("Bad Ending");
                    _hasEnding = true;
                    break;
                case 3:
                    //Referenced in PrinterScript's DestroyPrinter()
                    Debug.Log("Broke Ending");
                    _hasEnding = true;
                    break;
                default:
                    Debug.Log("No Such Ending Exists");
                    break;
            }

            UIManager.Instance.GameEnd(choice); // Runs code to load ending scene

            Time.timeScale = 0f;
        }
            
    }
}
