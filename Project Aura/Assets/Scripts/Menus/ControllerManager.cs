using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class ControllerManager : MonoBehaviour
{
   
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
