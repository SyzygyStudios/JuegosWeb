using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    private bool _leverContact;
    private bool _doorContact;

    private Collider2D contactGameObject;
    private bool _interactDoor;
    private bool _interactLever;


    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            _interactDoor = true;
        }
        
        if (Input.GetKeyDown("g"))
        {
            _interactLever = true;
        }
        
        if (_interactLever)
        {
            if (_leverContact)
            {
                contactGameObject.gameObject.GetComponent<LeverController>().OpenDoor();
            }

            _interactLever = false;
        }

        if (_interactDoor)
        {
            if (_doorContact)
            {
               
                contactGameObject.gameObject.GetComponent<DoorController>().changeScene();
            }

            _interactDoor = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lever"))
        {
            contactGameObject = other;
            _leverContact = true;
        }

        if (other.CompareTag("Door"))
        {
            Debug.Log("Tocando puerta");
            contactGameObject = other;
            _doorContact = true;
            contactGameObject.gameObject.GetComponent<DoorController>().apearText();
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Lever"))
        {
            _leverContact = false;
        }

        if (other.CompareTag("Door"))
        {
            contactGameObject = other;
            _doorContact = false;
        }
    }

    public void Interact()
    {
            _interactDoor = true;
            _interactLever = true;
    }

    
}
