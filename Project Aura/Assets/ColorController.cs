using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    // Start is called before the first frame update
    private GameMetrics _gameMetrics;
    
    void Start()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
