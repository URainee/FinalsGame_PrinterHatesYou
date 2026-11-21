using System;
using UnityEngine;
using TMPro;


public class TimerScript : MonoBehaviour
{
    //*
    //Sourced from CocoCode
    //*

    private bool _timerActive;

    private float _currentTime;

    [SerializeField] private TextMeshProUGUI _currentTimeText;

    //*
    //Lets the GameManager instance assign a number to startMinutes
    //Used to facilitate the gathering off all important, universal 
    //variables into one manager script.
    //*

    private int _startMinutes;

    void Start()
    {
        _timerActive = false;

        _startMinutes = GameManager.Instance.StartMinutes;
        _currentTime = _startMinutes * 60;

        StartTimer();
    }

    private void Update()
    {
        if (_timerActive == true)
        {
            _currentTime = _currentTime - Time.deltaTime;

            if (_currentTime < 0)
            {
                StopTimer();
                //Start(); 
                //You can run Start again to reset time
                //Closed though, as we only need to run through this code once.
                GameManager.Instance.PlayEnding(2) // Plays the BadEnding
;            }
        }
        TimeSpan time = TimeSpan.FromSeconds(_currentTime); // converts seconds to be readable as minutes or hours

        _currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
 
    }

    private void StartTimer()
    {
        _timerActive = true;
    }

    private void StopTimer()
    {
        _timerActive = false;
    }
}
