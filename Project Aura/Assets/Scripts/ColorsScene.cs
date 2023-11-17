using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorsScene : MonoBehaviour
{
    private bool blue = false;
    private bool green = false;
    private bool red = false;
    private bool yellow = false;
    private bool purple = false;
    

    public GameObject color;

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene == SceneManager.GetSceneByName("Mundo 1"))
        {
            blue = true;
        }
        if (scene == SceneManager.GetSceneByName("Mundo 2"))
        {
            red = true;
        }
        if (scene == SceneManager.GetSceneByName("Mundo 3"))
        {
            green = true;
        }
        if (scene == SceneManager.GetSceneByName("Mundo 4"))
        {
            yellow = true;
        }
        if (scene == SceneManager.GetSceneByName("Mundo 5"))
        {
            purple = true;
        }
        

        Debug.Log(scene);

        if(blue)
            GameObject.FindWithTag("BluePower").SetActive(true); 
        if(red)
            GameObject.FindWithTag("RedPower").SetActive(true);
        if (yellow)
            GameObject.FindWithTag("YellowPower").SetActive(true);
        if (green)
            GameObject.FindWithTag("GreenPower").SetActive(true);
        if (purple)
            GameObject.FindWithTag("PurplePower").SetActive(true);


    }

   
}
