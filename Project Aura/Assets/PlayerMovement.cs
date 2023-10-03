using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed, acceleration, decceleration, coyoteTime, coyoteTimeCounter, jumpBuffer, jumpBufferCounter, fallGravityScaleMultiplier, gravityScale;
    [SerializeField] private float jumpForce;
    private bool onAir;
    private bool jump;
    private Rigidbody2D playerRigidBody;
    float horizontalMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        onAir = false;
    }
    void FixedUpdate()
    {
        if(horizontalMove!=0)
        {
            //if(onAir) horizontalMove/=1.5f;
            //playerRigidBody.AddForce(new Vector2(horizontalMove * speed,0f), ForceMode2D.Impulse);
            /* SISTEMA DE SALTO PROTOTIPO, TENGO QUE MIRAR BIEN COMO CONFIGURARLO */
            float targetSpeed = horizontalMove * speed;
            float speedDiff = targetSpeed - playerRigidBody.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f)? acceleration : decceleration;
            float movement = speedDiff * accelRate;
            playerRigidBody.AddForce(movement * Vector2.right, ForceMode2D.Force);
            /**/
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
        horizontalMove = Input.GetAxisRaw("Horizontal");

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
}
