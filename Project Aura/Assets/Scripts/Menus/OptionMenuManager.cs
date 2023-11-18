using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class OptionMenuManager : MonoBehaviour
{

    public Slider sliderVolume;
    public float ValueVolume;
    public Image imageMute;

    public Slider sliderShine;
    public float ValueShine;
    public Image panelShine;

    public Toggle toggle;

    public GameObject opcionsMenu;
    public GameObject mainMenu;


    void Start()
    {
        sliderVolume.value = PlayerPrefs.GetFloat("volumenAudio", 0.1f);
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
        opcionsMenu.SetActive(false);
        mainMenu.SetActive(true);
        //SceneManager.LoadScene("MainMenu");
    }
}
