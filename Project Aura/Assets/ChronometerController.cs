using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChronometerController : MonoBehaviour
{

    private float _timeElapsed;
    [SerializeField] private TextMeshProUGUI textSeconds;
    [SerializeField] private TextMeshProUGUI textMinutes;

    private void Start()
    {
        _timeElapsed = 0;
    }

    void Update()
    {
        _timeElapsed += Time.deltaTime;
        PrintTimer();
    }

    public void ResetTimer()
    {
        _timeElapsed = 0;
    }
    
    public float GetTimer()
    {
        return _timeElapsed;
    }

    public void PrintTimer()
    {
        int _seconds = (int) _timeElapsed;
        int _minutes = 0;
        while (_seconds > 60)
        {
            _seconds -= 60;
            _minutes += 1;
        }
        Debug.Log(_seconds);
        Debug.Log(_minutes);
        if (_minutes < 10)
        {
            textMinutes.text = ("0") + _minutes;
        }
        else
        {
            textMinutes.text = _minutes.ToString();
        }

        if (_seconds < 10)
        {
            textSeconds.text = ("0") + _seconds;
        }
        else
        {
            textSeconds.text = _seconds.ToString();
        }
    }
}
