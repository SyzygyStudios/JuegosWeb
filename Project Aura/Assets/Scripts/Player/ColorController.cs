using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private int _hoverColor;
    private Color _auxColor;
    private Color _finalColor;
    private Color _originalColor;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _red;
    [SerializeField] private Color _green;
    [SerializeField] private Color _yellow;
    [SerializeField] private Color _cyan;
    [SerializeField] private Color _magenta;
    private bool _asignColor;

    private void Start()
    {
        _blue = new Color(116f/255f, 64f/255f, 228f/255f);
        _red = new Color(228f/255f, 74f/255f, 94f/255f);
        _green = new Color(74f/255f, 185f/255f, 102f/255f);
        _yellow = new Color(255f/255f, 217f/255f, 0);
        _cyan = new Color(56f/255f, 243f/255f, 243f/255f);
        _magenta = new Color(184f/255f, 56f/255f, 231f/255f);
        _auxColor = Color.white;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
        _finalColor = _originalColor;
    }

    void Update()
    {
        _spriteRenderer.color = _finalColor;
        if (Input.GetKeyDown("e"))
        {
            _asignColor = true;
        }
        if(_asignColor)
        {
            _asignColor = false;
            _finalColor = _auxColor;
            if (_hoverColor >= 1 && _hoverColor <= 6)
            {
                gameObject.GetComponent<PlayerMovement>().SetColor(color: _hoverColor);
            }
        }
    }

    public void AssignColor()
    {
        _asignColor = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("BluePower"))
        {
            _auxColor = _blue;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 1;
        }
        else if (other.CompareTag("RedPower"))
        {
            _auxColor = _red;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 2;
        }
        else if (other.CompareTag("GreenPower"))
        {
            _auxColor = _green;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 3;
        }
        else if (other.CompareTag("CianPower"))
        {
            _auxColor = _cyan;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 4;
        }
        else if (other.CompareTag("PurplePower"))
        {
            _auxColor = _magenta;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 5;
        }
        else if (other.CompareTag("YellowPower"))
        {
            _auxColor = _yellow;
            if(_auxColor!= _finalColor) gameObject.GetComponent<PlayerMovement>().SetHoverPower(true);
            _hoverColor = 6;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BluePower") || other.CompareTag("GreenPower") || other.CompareTag("CianPower") || other.CompareTag("PurplePower") || other.CompareTag("RedPower") || other.CompareTag("YellowPower"))
        {
            gameObject.GetComponent<PlayerMovement>().SetHoverPower(false);
        }
    }

    public void DeleteColor()
    {
        _auxColor = Color.white;
        _finalColor = Color.white;
        _hoverColor = 0;
        GetComponent<PlayerMovement>().SetColor(color: _hoverColor);
    }
}
