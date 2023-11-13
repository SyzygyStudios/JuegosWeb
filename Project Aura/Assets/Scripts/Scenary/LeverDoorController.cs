using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
