using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollorController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private int _hoverColor;
    private Color _auxColor;
    private Color _finalColor;
    private Color _originalColor;
    private Color _blue;
    private Color _red;
    private Color _green;
    private Color _yellow;
    private Color _cyan;
    private Color _magenta;

    private void Start()
    {
        _blue = new Color(116f/255f, 64f/255f, 228f/255f);
        _red = new Color(228f/255f, 74f/255f, 94f/255f);
        _green = new Color(74f/255f, 185f/255f, 102f/255f);
        _yellow = new Color(255f/255f, 217f/255f, 0);
        _cyan = new Color(56f/255f, 243f/255f, 243f/255f);
        _magenta = new Color(184f/255f, 56f/255f, 231f/255f);
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _finalColor = _originalColor;
    }

    void Update()
    {
        _spriteRenderer.color = _finalColor;
        if (Input.GetKeyDown("e"))
        {
            _finalColor = _auxColor;
            Debug.Log("Asigno el " + _hoverColor);
            GetComponent<PlayerMovement>().SetColor(color: _hoverColor);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BluePower"))
        {
            Debug.Log("Toco el azul");
            _auxColor = _blue;
            _hoverColor = 1;
        }
        else if (other.CompareTag("RedPower"))
        {
            Debug.Log("Toco el rojo");
            _auxColor = _red;
            _hoverColor = 2;
        }
        else if (other.CompareTag("GreenPower"))
        {
            Debug.Log("Toco el verde");
            _auxColor = _green;
            _hoverColor = 3;
        }
        else if (other.CompareTag("CianPower"))
        {
            Debug.Log("Toco el cian");
            _auxColor = _cyan;
            _hoverColor = 4;
        }
        else if (other.CompareTag("PurplePower"))
        {
            Debug.Log("Toco el magenta");
            _auxColor = _magenta;
            _hoverColor = 5;
        }
        else if (other.CompareTag("YellowPower"))
        {
            Debug.Log("Toco el amarillo");
            _auxColor = _yellow;
            _hoverColor = 6;
        }
    }
}
