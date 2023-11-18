using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int world;
    [SerializeField] private GameMetrics _gameMetrics;
    private ChronometerController _chronometer;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
        _chronometer = FindObjectOfType<ChronometerController>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Contacto con el jugador");
            _gameMetrics.SetLevel(level + ((world-1)*3));
            if (level != 1 && world != 1)
            {
                _gameMetrics.SetLevel(level -1);
                _gameMetrics.CompleteWorld();
                _gameMetrics.SetLevel(level + 1);
            }
        }
    }
}
