using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int _worldsCompleted;
    public bool[] _completed;
    public int[] _jumpsMade;
    public int[] _startsCollected;
    public float[] _timeCompletion;
    
    
    public GameData(GameMetrics gameMetrics)
    {
        for (int i = 0; i < 16; i++)
        {
            _completed[i] = gameMetrics.GetCompletedLevel(i);
            _jumpsMade[i] = gameMetrics.GetJumpsLevel(i);
            _startsCollected[i] = gameMetrics.GetStarsLevel(i);
            _timeCompletion[i] = gameMetrics.GetTimeLevel(i);
        }
        _worldsCompleted = gameMetrics.GetWorldsCompleted();
    }
    
}
