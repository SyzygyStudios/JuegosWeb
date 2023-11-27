using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private LeverDoorController _ldc;

    public void OpenDoor()
    {
        Debug.Log("Abro la puerta 2");
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if(!_ldc.IsDestroyed()) _ldc.Open();
    }
}
