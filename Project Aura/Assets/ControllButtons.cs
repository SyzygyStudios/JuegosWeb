using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllButtons : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private ColorController _colorController;
    
    void Start()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _colorController = FindObjectOfType<ColorController>();
    }

    // Update is called once per frame
    public void Jump()
    {
        _playerMovement.ActivateJump();
    }
    public void Ability()
    {
        _playerMovement.ActivateAbility();
    }
    public void Interact()
    {
        _colorController.AssignColor();
    }
}
