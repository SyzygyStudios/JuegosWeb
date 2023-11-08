using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
        
    [Header("General Variables")]
    //Variables generales para el control del personaje
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private float abilityCooldown;
    [SerializeField] private float gravityScale;
    [SerializeField] private bool _grounded;
    private int _activeColor;
    private float abilityCooldownCounter;
    private float _lastVelocity;
    private bool airMove;
    private Rigidbody2D _rb;
    private Animator animator;
    

    /// Variables que controlan el movimiento al correr
    [Header("Run")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] Joystick joystick;
    private float _horizontalInput, _horizontalMove;
    
    /// Variables que controlan el salto
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float airFrictionMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float jumpCutGravityMultiplier;
    private bool _jumpCut;
    
    [Space(10)]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;
    private TrailRenderer _tr;
    
    [Space(10)]
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private Transform floorCheck;
    [SerializeField] private Transform roofCheck;
    private int doubleJump;
    
    [Header("Wall Jump")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private float checkTime;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private Transform wallCheck;
    private bool _isWallTouch;
    private bool _startWallJumping;
    private bool _isWallJumping;
    private bool _isSliding;
    private bool _changingWall;
    
    ///  Variables que controlan el dash y el rodar
    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime;
    private bool _canDash;
    private bool _startDash;
    private bool _isDashing;
    
    [Header("Roll")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    private bool _isRolling;
    private bool _canRoll;
    private bool _startRoll;
    
    [Header ("Gravity Ability")]
    [SerializeField] private bool _startGravity;
    [SerializeField] private float gravitySign;
    
    [Header ("Bomb Jump")]
    [SerializeField] private float bombJumpVelocity;
    private bool _isBombJumping;
    private bool _startBombJump;

    void Start()
    {
        InitializeVariables();
    }

    void FixedUpdate()
    {
        
        //CORRER
        
        if(_horizontalInput!=0)
        {
            Run();
            animator.SetBool("_isRunning", true);
        }
        else
        {
            animator.SetBool("_isRunning", false);
        }
        
        //SALTAR

        if(jumpBufferCounter>0f)
        {
            Jump();
        }
        
        //DASH
        
        if(_startDash)
        {
            _startDash = false;
            StartCoroutine(Dash());
        }
        
        //RODAR
        
        if(_startRoll)
        {
            _startRoll = false;
            StartCoroutine(Roll());
        }
        
        //GRAVEDAD
        
        if(_startGravity)
        {
            _startGravity = false;
            StartCoroutine(Gravity());
        }
        
        //SALTO BOMBA
        
        if(_startBombJump)
        {
            _startBombJump = false;
            BombJump();
        }
        
        //WALL JUMP
        
        if (_startWallJumping)
        {
            _startWallJumping = false;
            StartCoroutine(WallJump());
        }
        
        //DESLIZAR POR PARED

        if (_isSliding && gravityScale>0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
        }
        else if (_isSliding && gravityScale<0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, wallSlideSpeed);
        }
        
        //CONTROL DE GRAVEDAD AL CAER
        if (_rb.velocity.y < 0 && !_isDashing)
        {
            _rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else if (!_isDashing)
        {
            _rb.gravityScale = gravityScale;
        }

        //CONTROL DE ALTURA DE SALTO
        
        if (gravitySign > 0)
        {
            if (_jumpCut && _rb.velocity.y > 0 && !_grounded)
            {
                _rb.velocity -= (new Vector2(0,
                    (_rb.velocity.y / jumpCutGravityMultiplier)));
            }
        }
        else
        {
            if (_jumpCut && _rb.velocity.y < 0 && !_grounded)
            {
                _rb.velocity -= (new Vector2(0,
                    (_rb.velocity.y / jumpCutGravityMultiplier)));
            }
        }
        
    }

    void Update()
    {
        if (CanRun())
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal") != 0
                ? Input.GetAxisRaw("Horizontal")
                : joystick.Horizontal;
        }

        if(!_grounded)
        {
            coyoteTimeCounter-= Time.deltaTime;
        }

        if (!_grounded && _isWallTouch && _horizontalInput!=0 && _activeColor == 6 )
        {
            _isSliding = true;
        }
        else
        {
            _isSliding = false;
        }

        if(Input.GetKeyDown("space") && CanJump())
        {
            ActivateJump();
        }
        
        if(Input.GetKeyDown("space") && CanWallJump())
        {
            ActivateWallJump();
        }
        
        if (Input.GetKeyDown("q"))
        {
            ActivateAbility();
        }
        
        if(Input.GetKeyUp("space") && !_grounded)
        {
            ActivateJumpCut();
        }
        

        GroundCheck();
        WallCheck();
        
        jumpBufferCounter -= Time.deltaTime;
        abilityCooldownCounter += Time.deltaTime;
        gravitySign = (_rb.gravityScale / Mathf.Abs(_rb.gravityScale));
        _lastVelocity = _rb.velocity.x;
        Flip();
    }
    
    private void InitializeVariables()
    {
        animator = GetComponent<Animator>();
        _tr = GetComponent<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _grounded = false;
        _canDash = true;
        _isDashing = false;
        _startDash = false;
        _canRoll = true;
        _isRolling = false;
        _startRoll = false;
        _changingWall = false;
        doubleJump = 2;
    }

    public void ActivateJump()
    {
        jumpBufferCounter = jumpBuffer;
    }

    public void ActivateWallJump()
    {   
        _startWallJumping = true;
    }

    public void ActivateJumpCut()
    {
        _jumpCut = true;
    }
    
    public void ActivateAbility()
    {
        if (abilityCooldownCounter > abilityCooldown)
        {
            if (CanDash())
            {
                _startDash = true;
                abilityCooldownCounter = 0;
            }
            else if (CanRoll())
            {
                _startRoll = true;
                abilityCooldownCounter = 0;
            }
            else if (CanGravity())
            {
                _startGravity = true;
                abilityCooldownCounter = 0;
            }
            else if (CanBombJump())
            {
                _startBombJump = true;
                abilityCooldownCounter = 0;
            }
        }
    }
    
    private void Run()
    {
        if (!_grounded)
        {
            _horizontalMove = _horizontalInput * airFrictionMultiplier;
            if (_rb.velocity.x < 0 && _horizontalMove > 0 || _rb.velocity.x > 0 && _horizontalMove < 0 || airMove)
            {
                airMove = true;
                _horizontalMove = _horizontalMove / 1.5f;
            }
        }
        else
        {
            _horizontalMove = _horizontalInput;
        }
        float targetSpeed = _horizontalMove * speed;
        float speedDiff = targetSpeed - _rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = speedDiff * accelRate;
        _rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }
    
    private void Jump()
    {
        if (coyoteTimeCounter > 0f)
        {
            _jumpCut = false;
            doubleJump--;
            Debug.Log("Voy a saltar");
            _rb.AddForce(Vector2.up * (jumpForce * gravitySign), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            PreserveMomentum();
        }
        else if (doubleJump <= 2)
        {
            if (doubleJump == 1 && _activeColor != 1) return;
            _jumpCut = false;
            doubleJump = 0;
            _rb.AddForce(Vector2.up * ((jumpForce - _rb.velocity.y) * gravitySign),
                ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            PreserveMomentum();
        }
    }
    
    private IEnumerator Gravity()
    {
        Debug.Log("Se ejecuta gravedad");
        gravityScale = -gravityScale;
        yield return new WaitForSeconds(1);
    }

    private IEnumerator WallJump()
    {
        _isWallJumping = true;
        Debug.Log("WallJump");
        doubleJump--;
        gravityScale = 0;
        if (!_changingWall)
        {
            _rb.totalForce = Vector2.zero;
            _rb.AddForce(new Vector2(wallJumpForce.x * -(transform.localScale.x), wallJumpForce.y),
                ForceMode2D.Impulse);
        }
        else
        {
            _rb.totalForce = Vector2.zero;
            _rb.AddForce(new Vector2(wallJumpForce.x * (transform.localScale.x), wallJumpForce.y),
                ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(wallJumpDuration);
        gravityScale = 10;
        _isWallJumping = false;
    }
    
    private IEnumerator Dash()
    {
        float prevGravityScale = gravityScale;
        gravityScale = 0;
            _isDashing = true;
            _tr.emitting = true;
            var inputX = Input.GetAxisRaw("Horizontal");
            _rb.velocity = new Vector2(inputX, 0).normalized * dashForce;
            yield return new WaitForSeconds(dashingTime);
            if (!_grounded)
            {
                Debug.Log("ESTOY EN EL AIRE");
                _canDash = false;
            }

            _tr.emitting = false;
            gravityScale = prevGravityScale;
            _isDashing = false;
            
    }
    
    private IEnumerator Roll()
    {
        _isRolling = true;
        _rb.totalForce = Vector2.zero;
        _rb.velocity = new Vector2(_horizontalInput,0).normalized * rollForce;
        GetComponent<CapsuleCollider2D>().size /= 2;
        yield return new WaitForSeconds(rollTime);
        GetComponent<CapsuleCollider2D>().size *= 2;
        _isRolling = false;
    }

    private void BombJump()
    {
        _isBombJumping = true;
        _rb.velocity = new Vector2(0, -bombJumpVelocity * gravitySign);
    }

    private bool CanRun()
    {
        return !_isDashing && !_isRolling && !_isBombJumping;
    }

    private bool CanJump()
    {
        return (!_isDashing && !_isRolling && doubleJump>0 && !_isWallJumping && !_isBombJumping && !_isSliding);
    }
    
    private bool CanGravity()
    {
        return _canDash && _activeColor == 5;
    }
    
    private bool CanWallJump()
    {
        return _isSliding;
    }
    
    private bool CanDash()
    {
        return _canDash && _activeColor == 2;
    }
    
    private bool CanRoll()
    {
        return _canRoll && _grounded && _activeColor == 3;
    }
    
    private bool CanBombJump()
    {
        return !_grounded && _activeColor == 4;
    }
    
    private void GroundCheck()
    {
        if (gravitySign > 0)
        {
            _grounded = Physics2D.OverlapBox(floorCheck.position, new Vector2(0.5f, .1f), 0, floorLayer);
        }
        else
        {
            _grounded = Physics2D.OverlapBox(roofCheck.position, new Vector2(0.5f, .1f), 0, floorLayer);
        }
    }

    private void WallCheck()
    {
        if (_isWallTouch != Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer))
        {
            StartCoroutine(WallCheckChange());
        }
    }

    private IEnumerator WallCheckChange()
    {
        _changingWall = true;
        yield return new WaitForSeconds(checkTime);
        _changingWall = false;
        _isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer);
    }
    
    private void PreserveMomentum()
    {
        _rb.velocity = new Vector2(_lastVelocity, _rb.velocity.y);
    }
    
    private void Flip()
    {
        if (_rb.velocity.x > 3 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }else if(_rb.velocity.x < -3 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
} 
    
    public void SetColor(int color)
    {
        _activeColor = color;
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        
        //SI TOCA EL SUELO LE DECIMOS QUE MANTENGA EL MOMENTUM, QUE NO SE FRENE AL CAER, TAMBIEN REINICIAMOS VARIABLES DE HABILIDADES
        if(collision.CompareTag("Floor") || collision.CompareTag("ShatteredFloor"))
        {
            if (_isBombJumping)
            {
                _isBombJumping = false;
                if(collision.CompareTag("ShatteredFloor"))
                {
                    collision.gameObject.GetComponent<ShatteredFloor>().Break();
                }
            }
            doubleJump = 2;
            _canDash = true;
            _jumpCut = false;
            airMove = false;
            coyoteTimeCounter = coyoteTime;
        }
        if(collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("ShatteredFloor"))
        {
            PreserveMomentum();
        }
    }
    
}
