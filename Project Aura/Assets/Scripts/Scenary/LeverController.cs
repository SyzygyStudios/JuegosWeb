using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private LeverDoorController _ldc;
    // Start is called before the first frame update

    // Update is called once per frame
    public void OpenDoor()
    {
        Debug.Log("Abro la puerta 2");
        _ldc.Open();
    }
}
