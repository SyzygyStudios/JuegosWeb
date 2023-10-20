using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /// Variables que controlan el movimiento al correr
    [Header("Run")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;

    /// Variables que controlan el salto
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallGravityScaleMultiplier;
    [SerializeField] private float gravityScale;
    [SerializeField] private float airFrictionMultiplier;
    [SerializeField] private float airFrictionMultipliersdsd;

    [Space(10)]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;

    [Space(10)]
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter;


    private bool onAir;
    private Rigidbody2D playerRigidBody;
    float horizontalInput, horizontalMove;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        onAir = false;
    }

    void FixedUpdate()
    {
        if(horizontalInput!=0)
        {
            if(onAir) 
            {
                horizontalMove = horizontalInput * airFrictionMultiplier;
            }
            else
            {
                horizontalMove = horizontalInput;
            }
            float targetSpeed = horizontalMove * speed;
            float speedDiff = targetSpeed - playerRigidBody.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f)? acceleration : decceleration;
            float movement = speedDiff * accelRate;
            playerRigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
        }

        if(jumpBufferCounter>0f)
        {
            Jump();
        }

        if(playerRigidBody.velocity.y < 0)
        {
            playerRigidBody.gravityScale = gravityScale * fallGravityScaleMultiplier;
        }
        else 
        {
            playerRigidBody.gravityScale = gravityScale;
        }
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if(onAir)
        {
            coyoteTimeCounter-= Time.deltaTime;
        }

        if(Input.GetKeyDown("w"))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void Jump()
    {
            if(coyoteTimeCounter > 0f)
            {
                Debug.Log("Voy a saltar");
                playerRigidBody.AddForce(Vector2.up * (jumpForce - playerRigidBody.velocity.y), ForceMode2D.Impulse);
                coyoteTimeCounter = 0f;
                jumpBufferCounter = 0f;
            }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Floor")
        {
            Debug.Log("Toco suelo");
            onAir = false;
            coyoteTimeCounter = coyoteTime;
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.tag == "Floor")
        {
            Debug.Log("Salto");
            onAir = true;
        }
    }
}
