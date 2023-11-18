using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameMetrics _gameMetrics;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject red;
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject magenta;
    [SerializeField] private GameObject yellow;
    
    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
        Debug.Log("HOLAAAAA");
        if (_gameMetrics.GetCompletedWorld(0))
        {
            blue.SetActive(true);
        }
        if (_gameMetrics.GetCompletedWorld(1))
        {
            red.SetActive(true);
        }
        if (_gameMetrics.GetCompletedWorld(2))
        {
            green.SetActive(true);
        }
        if (_gameMetrics.GetCompletedWorld(3))
        {
            yellow.SetActive(true);
        }
        if (_gameMetrics.GetCompletedWorld(4))
        {
            magenta.SetActive(true);
        }
    }
}
