using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public int scene;
    //public GameObject Text;

    private void Start()
    {
        //Text.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            //Text.SetActive(true);

            SceneManager.LoadScene(scene);
        }
    }
}
