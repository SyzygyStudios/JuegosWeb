using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private string level;
    [SerializeField] private int fadeTime;
    private bool _displayed;

    void Start()
    {
        _displayed = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_displayed)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(SpawnText(fadeTime, textLevel));
                _displayed = true;
            }
        }
    }

    private IEnumerator EraseText(float t, TextMeshProUGUI i)
    {
        yield return new WaitForSeconds(2);
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    
    private IEnumerator SpawnText(float t, TextMeshProUGUI i)
    {
        i.text = "Nivel " + level;
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(EraseText(fadeTime, textLevel));
    }
    
}
