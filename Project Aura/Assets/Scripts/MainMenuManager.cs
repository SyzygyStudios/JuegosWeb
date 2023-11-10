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
    public GameObject canvaNick;
    public GameObject acceptBottom;

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

    void Start()
    {

    }


    public void Play() 
    {
        SceneManager.LoadScene("SelectLevel");

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void accept()
    {
        PlayerPrefs.SetString("name", inputText.text);

        mainMenu.SetActive(true);
        canvaNick.SetActive(false);
        acceptBottom.SetActive(false);

        Bot = true;

    }


}
