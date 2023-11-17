using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsController : MonoBehaviour
{
    private bool _leverContact;
    private bool _doorContact;

    private Collider2D contactGameObject;

    

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (_leverContact)
            {
                Debug.Log("Abro la puerta 1");
                contactGameObject.gameObject.GetComponent<LeverController>().OpenDoor();
            }

        }

        if (Input.GetKeyDown("g"))
        {
            if (_doorContact)
            {
               
                contactGameObject.gameObject.GetComponent<DoorController>().changeScene();
            }
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

    
}
