using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public InputField inputText;
    public Text Nick;
    public Image Light;
    
    

    public GameObject mainMenu;
    public GameObject opcionsMenu;
    public GameObject canvaNick;
    public GameObject acceptBottom;

    private Coroutine TimeCoroutine = null;

    private bool Bot = false;
    

    private void Awake()
    {
        Light.color = Color.red;
    }

    private void Update()
    {
        if (Nick.text.Length <= 2)
        {
            Light.color = Color.red;
            acceptBottom.SetActive(false);
        }

        if (Nick.text.Length > 2)
        {

            Light.color = Color.green;
            if(Bot == false)
            {
                acceptBottom.SetActive(true);
            }
            
        }
    }


    public void Play() 
    {
        FindObjectOfType<PlayerMovement>().EnableMovement();

        StartCoroutine(TimeCoroutine());

        IEnumerator TimeCoroutine()
        {
                yield return new WaitForSeconds(3);
                mainMenu.SetActive(false);

        }

    }


    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        opcionsMenu.SetActive(true);
        mainMenu.SetActive(false);
        //SceneManager.LoadScene("OptionsMenu");
    }

    public void Accept()
    {
        PlayerPrefs.SetString("name", inputText.text);
        mainMenu.SetActive(true);
        canvaNick.SetActive(false);
        acceptBottom.SetActive(false);

        Bot = true;

    }


}
