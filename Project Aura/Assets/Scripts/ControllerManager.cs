using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class ControllerManager : MonoBehaviour
{

    public void ChangeFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen cambiado a " + Screen.fullScreen);
    }
}
