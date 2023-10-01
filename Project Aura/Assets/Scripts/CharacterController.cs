using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float acceleration;
    private Rigidbody2D rigidBody;
    private bool looking = true;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        float inputMovement = Input.GetAxis("Horizontal");
      
        rigidBody.velocity = new Vector2(inputMovement*acceleration,rigidBody.velocity.y);

        Orientation(inputMovement);
    }

    void Orientation(float inputMovement)
    {
        if(looking == true && inputMovement < 0 || looking == false && inputMovement > 0)
        {
            looking = !looking;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y); ;
        }
    }
}
