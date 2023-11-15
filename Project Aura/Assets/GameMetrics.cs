using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetrics : MonoBehaviour
{
    struct WorldMetrics
    {
        public bool _completed;
        public int _jumpsMade;
        public int _starsCollected;
        public float _timeCompletion;
        
    }

    private WorldMetrics[] worlds;
    private int _jumpsInCurrent;
    private int _starsInCurrent;
    private ChronometerController chronometer;
    private int _currentWorld;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        chronometer = FindObjectOfType<ChronometerController>();
        worlds = new WorldMetrics[6];
    }
    
    public void CompleteLevel()
    {
        worlds[_currentWorld]._timeCompletion = chronometer.GetTimer();
        worlds[_currentWorld]._jumpsMade = _jumpsInCurrent;
        worlds[_currentWorld]._starsCollected = _starsInCurrent;
        worlds[_currentWorld]._completed = true;
    }

    public void SetLevel(int lvl)
    {
        _currentWorld = lvl;
    }

    public void AddJump()
    {
        _jumpsInCurrent++;
    }
    
    public void CollectStart()
    {
        _starsInCurrent++;
    }

    public bool GetCompletedWorld(int i)
    {
        return worlds[i]._completed;
    }
    
    public int GetJumpsWorld(int i)
    {
        return worlds[i]._jumpsMade;
    }
    
    public int GetStarsWorld(int i)
    {
        return worlds[i]._starsCollected;
    }
    
    public float GetTimeWorld(int i)
    {
        return worlds[i]._timeCompletion;
    }
    

    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        GameData data = SaveSystem.LoadData();
        for (int i = 0; i < 16; i++)
        {
            worlds[i]._completed = data._completed[i];
            worlds[i]._jumpsMade = data._jumpsMade[i];
            worlds[i]._starsCollected = data._startsCollected[i];
            worlds[i]._timeCompletion = data._timeCompletion[i];
        }
    }
}
