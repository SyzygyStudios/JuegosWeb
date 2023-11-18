using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalController : MonoBehaviour
{
    [SerializeField] private int world;
    [SerializeField] private GameObject canvas;
    [SerializeField] private ChronometerController chronometer;
    [SerializeField] private TextMeshProUGUI textMinutes;
    [SerializeField] private TextMeshProUGUI textSeconds;
    [SerializeField] private Text stars;
    [SerializeField] private Text jumps;
    private GameMetrics _gameMetrics;

    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
        chronometer = FindObjectOfType<ChronometerController>();
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        _gameMetrics.CompleteLevel();
        Debug.Log("Colisiono con" + other);
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
            int _seconds = (int) chronometer.GetTimer();
            int _minutes = 0;
            while (_seconds > 60)
            {
                _seconds -= 60;
                _minutes += 1;
            }
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
            stars.text = _gameMetrics.GetStarsWorld(world-1).ToString();
            jumps.text = _gameMetrics.GetJumpsWorld(world-1).ToString();
            chronometer.gameObject.SetActive(false);
        }
    }

    public void ReturnSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }
}
