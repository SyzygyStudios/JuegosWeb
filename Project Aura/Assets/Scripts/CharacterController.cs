using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public LayerMask layerFloor;

    private Rigidbody2D rigidBody;
    private bool looking = true;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        float inputMovement = Input.GetAxis("Horizontal");
      
        rigidBody.velocity = new Vector2(inputMovement*speed,rigidBody.velocity.y);

        Orientation(inputMovement);
    }

    bool stayFloor()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,new Vector2(boxCollider.bounds.size.x,boxCollider.bounds.size.y),0f,Vector2.down,0.2f,layerFloor);
        return raycastHit.collider != null;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && stayFloor())
        {
            rigidBody.AddForce(Vector2.up*jumpForce,ForceMode2D.Impulse);
        }
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
