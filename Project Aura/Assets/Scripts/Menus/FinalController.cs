using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class FinalController : MonoBehaviour
{
    public GameObject Menu;
    public void accept()
    {

        SceneManager.LoadScene("SelectLevel");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Contacto");
        if (other.CompareTag("Player"))
        {
            Menu.SetActive(true);
            
        }
    }
}
