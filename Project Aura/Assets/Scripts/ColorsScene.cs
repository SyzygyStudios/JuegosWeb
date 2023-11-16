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
    private bool cian = false;

    

    Scene scene;

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();

       
    }

    // Update is called once per frame
    void Update()
    {
        if(scene == SceneManager.GetSceneByName("Mundo 1"))
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
        if (scene == SceneManager.GetSceneByName("Mundo 6"))
        {
            cian = true;
        }
    }
}
