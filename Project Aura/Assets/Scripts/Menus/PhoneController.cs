using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject controllButtons;
    [SerializeField] private GameObject joystick;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GameObject textoMovil;
    [SerializeField] private GameObject textoPC;
    bool activeMovil;
    bool activePC;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        activeMovil = false;
        activePC = true;
    }

    public void SetActiveMenu()
    {
        controllButtons.SetActive(!controllButtons.activeSelf);
        SetJoystick();
        joystick.SetActive(!joystick.activeSelf);
        controllButtons.GetComponent<ControllButtons>().AssignVariables();
        if (textoPC != null && textoMovil != null)
        {
            textoMovil.SetActive(!textoMovil.activeSelf);
            textoPC.SetActive(!textoPC.activeSelf);
            activeMovil = textoMovil.activeSelf;
            activePC = textoPC.activeSelf;
        }
    }

    public void SetPlayer()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        controllButtons.GetComponent<ControllButtons>().AssignVariables();
    }

    public void SetJoystick()
    {
        _playerMovement.SetJoystick(joystick.GetComponent<Joystick>(), controllButtons.activeSelf);
    }
    public bool GetActive()
    {
        return joystick.activeSelf;
    }

    public void SetText()
    {
        textoMovil = GameObject.FindObjectOfType<TextoMovil>().gameObject;
        textoPC = FindObjectOfType<TextoPC>().gameObject;
        textoMovil.SetActive(activeMovil);
        textoPC.SetActive(activePC);
    }
}
