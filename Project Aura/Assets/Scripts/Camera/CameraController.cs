using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f);
    [SerializeField] private float smoothTime = 0.25f;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    


    bool movement = false;

    [SerializeField] private GameObject MainMenu;


    private void Update()
    {   
        
        Vector3 targetPosition = target.position + offset;

        if (movement)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
           Debug.Log("movieendome");
            

            
        }

       /* if (transform.position.x == target.position.x)
        {
            Debug.Log("termine");

            MainMenu.SetActive(false);
        }

        Debug.Log("camera"+ transform.position);
        Debug.Log("target" + target.position);
       */



    }

    public void Play()
    {
        movement = true;
                
    }



}
