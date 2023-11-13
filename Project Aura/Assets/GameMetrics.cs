using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetrics : MonoBehaviour
{
    struct LevelMetrics
    {
        public bool _completed;
        public int _jumpsMade;
        public int _starsCollected;
        public float _timeCompletion;
        
    }

    [SerializeField] private LevelMetrics[] levels;
    [SerializeField] private int _currentLevel;
    private int _jumpsInCurrent;
    private int _starsInCurrent;
    private ChronometerController chronometer;
    
    private int _currentWorld;
    private int worldsCompleted;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        chronometer = FindObjectOfType<ChronometerController>();
        levels = new LevelMetrics[16];
    }
    
    public void CompleteLevel()
    {
        levels[_currentLevel]._timeCompletion = chronometer.GetTimer();
        levels[_currentLevel]._jumpsMade = _jumpsInCurrent;
        levels[_currentLevel]._starsCollected = _starsInCurrent;
        levels[_currentLevel]._completed = true;
    }
    
    public void CompleteWorld()
    {
        worldsCompleted++;
    }

    public void SetLevel(int lvl)
    {
        _currentLevel = lvl;
    }

    public void AddJump()
    {
        _jumpsInCurrent++;
    }
    
    public void CollectStart()
    {
        _starsInCurrent++;
    }
}
