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
    [SerializeField] private TextMeshProUGUI minutes;
    [SerializeField] private TextMeshProUGUI seconds;
    [SerializeField] private Text jumps;
    [SerializeField] private Text stars;
    [SerializeField] private ChronometerController _chronometer;
    [SerializeField] private GameMetrics _gameMetrics;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _prevTime;
    [SerializeField] private Animator anim;
    private bool collide;




    void Start()
    {
        collide = false;
        _gameMetrics = FindObjectOfType<GameMetrics>();
        _chronometer = FindObjectOfType<ChronometerController>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isStart",true);

            _playerMovement.DisableMovement();
            _prevTime = Time.timeScale;
            //Time.timeScale = 0;
            _chronometer.gameObject.SetActive(false);
            if (collide == false)
            {
                _gameMetrics.SetCurrentWorld(world - 1);
                _gameMetrics.CompleteWorld();
                collide = true;
            }
            canvas.gameObject.SetActive(true);
            int _seconds = (int) _gameMetrics.GetTimeWorld(world - 1);
            int _minutes = 0;
            while (_seconds >= 60)
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
        }
    }

    public void ReturnSelector()
    {
        Time.timeScale = _prevTime;
        _gameMetrics.UnlockPower(world);
        SceneManager.LoadScene("SelectLevel");
    }
}
