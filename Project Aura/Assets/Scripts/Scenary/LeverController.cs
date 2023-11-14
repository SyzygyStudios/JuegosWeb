using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private LeverDoorController _ldc;

    public void OpenDoor()
    {
        Debug.Log("Abro la puerta 2");
        _ldc.Open();
    }
}
