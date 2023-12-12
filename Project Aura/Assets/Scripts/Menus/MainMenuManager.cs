using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private InputField Age;
    [SerializeField] private Text AgeText;
    [SerializeField] private Image Light2;

    [SerializeField] private GameObject SexCanvas;
    [SerializeField] private Button Sexbottom1;
    [SerializeField] private Button Sexbottom2;


    [SerializeField] private InputField inputText;
    [SerializeField] private Text Nick;
    [SerializeField] private Image Light;

    [SerializeField] private Toggle toggleW;
    [SerializeField] private Toggle toggleM;


    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject opcionsMenu;
    [SerializeField] private GameObject canvaNick;
    [SerializeField] private GameObject acceptBottom;
    [SerializeField] private GameObject CanvaAge;

    [SerializeField] private AudioClip ButtomEffect;
    [SerializeField] private AudioSource audioSourceEffects;

    [SerializeField] private Animator anim;

    private Coroutine TimeCoroutine = null;

    private bool Bot = false;
    private bool ButtomWomen = false;
    private bool ButtomMen = false;


    private void Awake()
    {
        Light.color = Color.red;
        Light2.color = Color.red;
    }

    private void Update()
    {
        float width = (float)Screen.width;
        float height = (float)Screen.height;
        float scale =  width / height;
        float originalScale = 16f / 9f;
        float multiplier = originalScale / scale;
        transform.localScale = new Vector3(0.0185f / multiplier, 0.0185f / multiplier, 1);
  
        if (AgeText.text.Length == 0)
        {
            Light2.color = Color.red;
            acceptBottom.SetActive(false);
        }
        else
        {

            Light2.color = Color.green;
        }

        if (Nick.text.Length <= 2)
        {
            Light.color = Color.red;
            acceptBottom.SetActive(false);
        }

        if (Nick.text.Length > 2)
        {         
            Light.color = Color.green;
            if(Bot == false && Light2.color == Color.green && ButtomMen || ButtomWomen)
            {
                acceptBottom.SetActive(true);
            }
            
        }


    }


    public void Play() 
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

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
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        Application.Quit();
    }

    public void Options()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        opcionsMenu.SetActive(true);
        mainMenu.SetActive(false);

        anim.SetBool("isEntry", true);
        //SceneManager.LoadScene("OptionsMenu");
    }

    public void Accept()
    {

        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        PlayerPrefs.SetString("name", inputText.text);

        PlayerPrefs.SetString("age", Age.text);

        mainMenu.SetActive(true);
        canvaNick.SetActive(false);
        acceptBottom.SetActive(false);
        SexCanvas.SetActive(false);
        CanvaAge.SetActive(false);

        Bot = true;

    }

    public void WomenCount()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;
        toggleW.isOn = true;
        toggleM.isOn = false;
       

        ButtomWomen = true;
        ButtomMen = false;

        Debug.Log(ButtomMen);
        Debug.Log(ButtomWomen);

    }

    public void MenCount()
    {

        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;
        toggleM.isOn = true;
        toggleW.isOn = false;
        

        ButtomMen = true;
        ButtomWomen = false;

        Debug.Log(ButtomMen);
        Debug.Log(ButtomWomen);

    }


}
