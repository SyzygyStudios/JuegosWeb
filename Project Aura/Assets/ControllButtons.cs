using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllButtons : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private ColorController _colorController;
    private InteractionsController _interactionsController;
    
    void Start()
    {
        _interactionsController = FindObjectOfType<InteractionsController>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _colorController = FindObjectOfType<ColorController>();
    }

    // Update is called once per frame
    public void Jump()
    {
        if(_playerMovement.CanJump()) _playerMovement.ActivateJump();
    }
    public void Ability()
    {
        _playerMovement.ActivateAbility();
    }
    public void Interact()
    {
        _colorController.AssignColor();
        _interactionsController.Interact();
    }
}
