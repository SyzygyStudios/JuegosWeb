using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool[] _completed;
    public int[] _jumpsMade;
    public int[] _startsCollected;
    public float[] _timeCompletion;
    
    
    public GameData(GameMetrics gameMetrics)
    {
        for (int i = 0; i < 16; i++)
        {
            _completed[i] = gameMetrics.GetCompletedWorld(i);
            _jumpsMade[i] = gameMetrics.GetJumpsWorld(i);
            _startsCollected[i] = gameMetrics.GetStarsWorld(i);
            _timeCompletion[i] = gameMetrics.GetTimeWorld(i);
        }
    }
    
}
