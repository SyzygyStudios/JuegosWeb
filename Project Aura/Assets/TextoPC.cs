using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoPC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindAnyObjectByType<PhoneController>().SetText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
