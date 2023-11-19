using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public int scene;
    public GameObject interactionText;

    
    public void changeScene()
    {
        Debug.Log("Cambiando");
        SceneManager.LoadScene(scene);
        
    }

    public void apearText()
    {
        Debug.Log("Texto");
        interactionText.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("saliendo");
            interactionText.SetActive(false);
        }
    }
}
