using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private int world;
    [SerializeField] private GameMetrics _gameMetrics;
    [SerializeField] private TextMeshProUGUI minutes;
    [SerializeField] private TextMeshProUGUI seconds;
    [SerializeField] private Text jumps;
    [SerializeField] private Text stars;

    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _gameMetrics.CompleteWorld();
            canvas.gameObject.SetActive(true);
            int _seconds = (int) _gameMetrics.GetTimeWorld(world - 1);
            int _minutes = 0;
            while (_seconds > 60)
            {
                _seconds -= 60;
                _minutes += 1;
            }
            if (_minutes < 10)
            {
                minutes.text = ("0") + _minutes;
            }
            else
            {
                minutes.text = _minutes.ToString();
            }

            if (_seconds < 10)
            {
                seconds.text = ("0") + _seconds;
            }
            else
            {
                seconds.text = _seconds.ToString();
            }
            jumps.text = _gameMetrics.GetJumpsWorld(world - 1).ToString();
            stars.text = _gameMetrics.GetStarsWorld(world - 1).ToString();
            SceneManager.LoadScene("SelectLevel");
        }
    }

    public void ReturnSelector()
    {
        SceneManager.LoadScene("SelectLevel");
        _gameMetrics.UnlockPower(world);
    }
}
