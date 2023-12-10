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

    [SerializeField] private WorldMetrics[] worlds;
    [SerializeField]private int _jumpsInCurrent;
    [SerializeField]private int _starsInCurrent;
    [SerializeField]private ChronometerController chronometer;
    [SerializeField]private bool[] _colorsUnlocked;
    [SerializeField]private int _currentWorld;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        worlds = new WorldMetrics[6];
        _colorsUnlocked = new bool[6];
    }
    
    public void CompleteWorld()
    {
        worlds[_currentWorld]._timeCompletion = chronometer.GetTimer();
        worlds[_currentWorld]._jumpsMade = _jumpsInCurrent;
        worlds[_currentWorld]._starsCollected = _starsInCurrent;
        worlds[_currentWorld]._completed = true;
    }

    public void SetCurrentWorld(int world)
    {
        _currentWorld = world;
    }

    public void AddJump()
    {
        _jumpsInCurrent++;
    }
    
    public void CollectStart()
    {
        _starsInCurrent++;
    }

    public void SetChronometer(ChronometerController ch)
    {
        chronometer = ch;
    }

    public bool GetCompletedWorld(int j)
    {
        return worlds[j]._completed;
    }

    public int GetLastCompletedWorld()
    {
        int i = 0;
        foreach (WorldMetrics a in worlds)
        {
            if (a._completed)
            {
                i++;
            }
        }

        return i;
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

    public void UnlockPower(int i)
    {
        for (int j = 0; j < i; j++)
        {
            _colorsUnlocked[j] = true;
        }
    }

    public bool CheckPower(int i)
    {
        if (_colorsUnlocked[i] == true)
        {
            return true;
        }
        else
        {
            return false;
        }
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
