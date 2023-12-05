using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllButtons : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ColorController _colorController;
    [SerializeField] private InteractionsController _interactionsController;
    public void AssignVariables()
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
