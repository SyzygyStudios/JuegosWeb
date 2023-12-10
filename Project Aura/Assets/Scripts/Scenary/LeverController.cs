using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private LeverDoorController _ldc;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        Debug.Log("Abro la puerta 2");
        anim.SetBool("isOpen", true);
        //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (!_ldc.IsDestroyed())
        {
            FindObjectOfType<CameraShake>().ShakeCamera();
            _ldc.Open();
        }
    }
}
