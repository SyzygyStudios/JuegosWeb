using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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
    
    /// <summary>
    ///  Variables que controlan el dash y el rodar
    /// </summary>
    [SerializeField] private float dashForce;
    [SerializeField] private float rollForce;
    [SerializeField] private float dashingTime;
    [SerializeField] private float rollTime;
    private bool isDashing;
    private bool canDash;
    private bool isRolling;
    private bool canRoll;

    [Space(10)]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;
    private TrailRenderer tr;
    
    [Space(10)]
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private int doubleJump;
    private Vector2 lastVelocity;


    private bool onAir;
    //EN PROCESO //private bool stopCharacter;
    private Rigidbody2D playerRigidBody;
    float horizontalInput, horizontalMove;

    void Start()
    {
        lastVelocity = new Vector2();
        tr = GetComponent<TrailRenderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        onAir = false;
        canDash = true;
        canRoll = true;
        doubleJump = 2;
    }

    void FixedUpdate()
    {
        lastVelocity = playerRigidBody.velocity;
        if(horizontalInput!=0 && !isDashing)
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

        if(jumpBufferCounter>0f && !isDashing && doubleJump>0)
        {
            Jump();
        }
        
        if(isDashing && canDash)
        {
            isDashing = false;
            StartCoroutine(Dash());
        }
        
        if(isRolling && canRoll)
        {
            isRolling = false;
            StartCoroutine(Roll());
        }

        if(playerRigidBody.velocity.y < 0 && !isDashing)
        {
            playerRigidBody.gravityScale = gravityScale * fallGravityScaleMultiplier;
        }
        else if(!isDashing)
        {
            playerRigidBody.gravityScale = gravityScale;
        }

        //if (stopCharacter && jumpBufferCounter<=0)
        //{
        //    stopCharacter = false;
        //    if(!onAir) playerRigidBody.velocity = Vector2.zero;
        //}
        
        
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        
        ///EN PROCESO
        ////if (Input.GetAxisRaw("Horizontal") == 0)
        ////{
        ////    stopCharacter = true;
        ////}

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
        if (Input.GetKeyDown("x") && canDash)
        {
            isDashing = true;
        }
        if (Input.GetKeyDown("z") && canRoll && !onAir)
        {
            isRolling = true;
        }
    }

    private void Jump()
    {
        Debug.Log("SALTO");
        if (onAir)
        {
            doubleJump--;
            playerRigidBody.totalForce = Vector2.zero;
            Debug.Log("Voy a saltar");
            playerRigidBody.AddForce(Vector2.up * (jumpForce*1.1f - playerRigidBody.velocity.y), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }else if (coyoteTimeCounter > 0f)
        {
            doubleJump--;
            Debug.Log("Voy a saltar");
            playerRigidBody.AddForce(Vector2.up * (jumpForce - playerRigidBody.velocity.y), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
    }
    
    private IEnumerator Dash()
    {
            tr.emitting = true;
            Debug.Log("Voy a dash");
            Debug.Log(playerRigidBody.velocity.normalized);
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");
            playerRigidBody.gravityScale = 0;
            playerRigidBody.totalForce = new Vector2(0, 0);
            playerRigidBody.velocity = new Vector2(0, 0);
            playerRigidBody.velocity = new Vector2(inputX,inputY).normalized * dashForce;
            yield return new WaitForSeconds(dashingTime);
            playerRigidBody.gravityScale = gravityScale;
            if (onAir)
            {
                canDash = false;
            }
            tr.emitting = false;
    }
    
    private IEnumerator Roll()
    {
        tr.emitting = true;
        Debug.Log("Voy a roll");
        Debug.Log(playerRigidBody.velocity.normalized);
        var inputX = Input.GetAxisRaw("Horizontal");
        playerRigidBody.gravityScale = 0;
        playerRigidBody.totalForce = new Vector2(0, 0);
        playerRigidBody.velocity = new Vector2(0, 0);
        playerRigidBody.velocity = new Vector2(inputX,0).normalized * rollForce;
        this.GetComponent<CapsuleCollider2D>().size /= 2;
        yield return new WaitForSeconds(rollTime);
        this.GetComponent<CapsuleCollider2D>().size *= 2;
        playerRigidBody.gravityScale = gravityScale;
        if (onAir)
        {
            canDash = false;
        }
        tr.emitting = false;
    }
    
    void PreserveMomentumOnImpact()
    {
        if (lastVelocity.y < -5)
            playerRigidBody.velocity = new Vector2(lastVelocity.x, playerRigidBody.velocity.y);
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Floor")
        {
            PreserveMomentumOnImpact();
            doubleJump = 2;
            Debug.Log("Toco suelo");
            onAir = false;
            canDash = true;
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
