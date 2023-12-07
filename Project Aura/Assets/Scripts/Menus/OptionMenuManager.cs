using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class OptionMenuManager : MonoBehaviour
{

    [SerializeField] private Slider sliderVolume;
    private float ValueVolume = 0.5f;
    [SerializeField] private Image imageMute;

    [SerializeField] private Slider sliderShine;
    [SerializeField] private float ValueShine;
    [SerializeField] private Image panelShine;

    [SerializeField] private Toggle toggle;

    [SerializeField] private GameObject opcionsButton;
    [SerializeField] private GameObject selectButton;

    [SerializeField] private GameObject opcionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject AnimationMenu;


    [SerializeField] private PhoneController _phoneController;

    [SerializeField] private AudioClip ButtomEffect;
    [SerializeField] private AudioSource audioSourceEffects;

    [SerializeField] private Animator anim;

    private Coroutine TimeCoroutine = null;



    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        sliderVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.3f);
        AudioListener.volume = sliderVolume.value;
        imMute();

        sliderShine.value = PlayerPrefs.GetFloat("brillo", 0.0f);
        panelShine.color = new Color(panelShine.color.r, panelShine.color.g, panelShine.color.b, sliderShine.value);

        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }


        
        //opcionsButton.SetActive(false);
        
        
        
    }

    void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            if(!opcionsMenu.activeSelf)
            opcionsButton.SetActive(true);
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu") && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("SelectLevel"))
        {
            selectButton.SetActive(true);
        }


    }

    public void ChangeSliderVolume(float value)
    {
        ValueVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", ValueVolume);
        AudioListener.volume = sliderVolume.value;
        imMute();
    }

    public void ChangeSliderShine(float value)
    {
        ValueShine = value;
        PlayerPrefs.SetFloat("brillo", ValueShine);
        panelShine.color = new Color(panelShine.color.r, panelShine.color.g, panelShine.color.b, sliderShine.value);
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        Screen.fullScreen = fullScreen;
    }


    public void imMute()
    {
        if(ValueVolume == 0)
        {
           imageMute.enabled = true;
        }
        else
        {
           imageMute.enabled = false;
        }
    }


    public void Return()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        

        anim.SetBool("isEntry", false);
        


        StartCoroutine(TimeCoroutine());

        IEnumerator TimeCoroutine()
        {
            yield return new WaitForSeconds(1);
            opcionsMenu.SetActive(false);

            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
            {
                mainMenu.SetActive(true);

            }
            else
            {
                opcionsButton.SetActive(true);
            }

        }
        

        

        

        

        //SceneManager.LoadScene("MainMenu");
    }

    public void activeMenu()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        opcionsButton.SetActive(false);
        opcionsMenu.SetActive(true);

        anim.SetBool("isEntry",true);

     



    }

    public void ChangeMovil()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        _phoneController.SetActiveMenu();
    }

    public void SelectorLevels()
    {
        audioSourceEffects.mute = true;
        audioSourceEffects.loop = false;
        audioSourceEffects.clip = ButtomEffect;
        audioSourceEffects.Play();
        audioSourceEffects.mute = false;

        opcionsMenu.SetActive(false);
        SceneManager.LoadScene("SelectLevel");
    }
}
