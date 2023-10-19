using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    public GameObject mainMenu;
    public GameObject nick;
    public GameObject acceptBottom;


    void Start()
    {
        mainMenu.SetActive(true);
        nick.SetActive(false);
        acceptBottom.SetActive(false);
    }


    public void Play() 
    {
        mainMenu.SetActive(false);
        nick.SetActive(true);
        acceptBottom.SetActive(true);

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }




}
