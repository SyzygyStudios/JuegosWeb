using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject controllButtons;
    [SerializeField] private GameObject joystick;
    [SerializeField] private PlayerMovement _playerMovement;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetActiveMenu()
    {
        controllButtons.SetActive(!controllButtons.activeSelf);
        SetJoystick();
        joystick.SetActive(!joystick.activeSelf);
        controllButtons.GetComponent<ControllButtons>().AssignVariables();
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
}
