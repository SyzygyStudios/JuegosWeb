using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollorController : MonoBehaviour
{
    private int _hoverColor;
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("Asigno el " + _hoverColor);
            GetComponent<PlayerMovement>().SetColor(color: _hoverColor);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BluePower"))
        {
            Debug.Log("Toco el azul");
            _hoverColor = 1;
        }
        else if (other.CompareTag("RedPower"))
        {
            Debug.Log("Toco el rojo");
            _hoverColor = 2;
        }
        else if (other.CompareTag("YellowPower"))
        {
            Debug.Log("Toco el amarillo");
            _hoverColor = 3;
        }
    }

    public void GrabColor()
    {
        Debug.Log("Asigno el " + _hoverColor);
        GetComponent<PlayerMovement>().SetColor(color: _hoverColor);
    }
}
