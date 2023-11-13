using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class OptionMenuManager : MonoBehaviour
{

    public Slider sliderVolume;
    public float sliderValueVolume;
    public Image imageMute;

    public Slider sliderShine;
    public float sliderValueShine;
    public Image panelShine;

    public Toggle toggle;


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
        sliderValueVolume = value;
        PlayerPrefs.SetFloat("volumenAudio", sliderValueVolume);
        AudioListener.volume = sliderVolume.value;
        imMute();
    }

    public void ChangeSliderShine(float value)
    {
        sliderValueShine = value;
        PlayerPrefs.SetFloat("brillo", sliderValueShine);
        panelShine.color = new Color(panelShine.color.r, panelShine.color.g, panelShine.color.b, sliderShine.value);
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }


    public void imMute()
    {
        if(sliderValueVolume == 0)
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
        SceneManager.LoadScene("MainMenu");
    }
}
