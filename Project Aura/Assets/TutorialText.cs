using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(false);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactionText.SetActive(true);
        }
    }
}
