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

    /// Variables que controlan el movimiento al correr
    [Header("Run")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    private float _horizontalInput, _horizontalMove;
    public Joystick joystick;
    /// Variables que controlan el salto
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravityScale;
    [SerializeField] private float airFrictionMultiplier;
    [SerializeField] private float fallGravityMultiplier;
    [SerializeField] private float jumpCutGravityMultiplier;
    [SerializeField] private bool _jumpCut;
    
    [Space(10)]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;
    private TrailRenderer _tr;
    
    [Space(10)]
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private int doubleJump;
    [SerializeField] private Transform floorCheck;
    [SerializeField] private Transform roofCheck;
    private Vector2 _lastVelocity;
    
    [Header("Wall Jump")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private bool _isWallTouch;
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private float wallJumpDuration;
    private bool _startWallJumping;
    private bool _isWallJumping;
    private bool _isSliding;
    
    ///  Variables que controlan el dash y el rodar
    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime;
    private bool _isDashing;
    private bool _canDash;
    private bool _startDash;
    
    [Header("Roll")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    private bool _isRolling;
    private bool _canRoll;
    private bool _startRoll;

    //Variables generales para el control del personaje
    [SerializeField] private bool _grounded;
    private Rigidbody2D _rb;
    private float abilityCooldownCounter;
    [SerializeField] private float abilityCooldown;
    [SerializeField] private int _activeColor;
    [SerializeField] private LayerMask floorLayer;
    [SerializeField] private bool _startGravity;
    [SerializeField] private bool airMove;
    [SerializeField] private bool _canGravity;
    [SerializeField] private float gravitySign;


    void Start()
    {
        _lastVelocity = new Vector2();
        _tr = GetComponent<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _grounded = false;
        _canDash = true;
        _isDashing = false;
        _startDash = false;
        _canRoll = true;
        _canGravity = true;
        _isRolling = false;
        _startRoll = false;
        doubleJump = 2;
    }

    void FixedUpdate()
    {
        
        if(_horizontalInput!=0 && !_isDashing && !_isRolling && !_isWallJumping)
        {
            Run();
        }

        if(jumpBufferCounter>0f && !_isDashing && !_isRolling && doubleJump>0 && !_isWallJumping)
        {
            Jump();
        }
        
        if(_startDash && _canDash)
        {
            _startDash = false;
            StartCoroutine(Dash());
        }
        
        if(_startRoll && _canRoll)
        {
            _startRoll = false;
            StartCoroutine(Roll());
        }
        
        if(_startGravity && _canGravity)
        {
            _startGravity = false;
            _canGravity = false;
            StartCoroutine(Gravity());
        }

        if (_isSliding && gravityScale>0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
        }
        
        if (_isSliding && gravityScale<0)
        {
            Debug.Log("Me deslizo");
            _rb.velocity = new Vector2(_rb.velocity.x, wallSlideSpeed);
        }
        
        if (_startWallJumping)
        {
            StartCoroutine(WallJump());
            _startWallJumping = false;
        }
        
        //SI LAS CONDICIONES PARA LOS DISTINTOS MOVIMIENTOS SE CUMPLEN SE EJECUTAN//
        if(_rb.velocity.y < 0 && !_isDashing)
        {
            _rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else if(!_isDashing)
        {
            _rb.gravityScale = gravityScale;
        }

        if (gravitySign > 0)
        {
            if (_jumpCut && _rb.velocity.y > 0 && _canGravity)
            {
                _rb.velocity -= (new Vector2(0,
                    (_rb.velocity.y / jumpCutGravityMultiplier)));
            }
        }
        else
        {
            if (_jumpCut && _rb.velocity.y < 0 && _canGravity)
            {
                _rb.velocity -= (new Vector2(0,
                    (_rb.velocity.y / jumpCutGravityMultiplier)));
            }
        }
       
        _lastVelocity = _rb.velocity;
        
    }

    void Update()
    {
        
        //SE COMPTRUEBA MEDIANTE BOOLEANOS SI SE CUMPLEN LAS CONDICIONES PARA LOS DISTINTOS MOVIMIENTOS//
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            _horizontalInput = joystick.Horizontal;
        }

        if(!_grounded)
        {
            coyoteTimeCounter-= Time.deltaTime;
        }

        if (!_grounded && _isWallTouch && _horizontalInput!=0)
        {
            _isSliding = true;
        }
        else
        {
            _isSliding = false;
        }

        if(Input.GetKeyDown("w") && !_isSliding)
        {
            ActivateJump();
        }
        if(Input.GetKeyDown("w") && _isSliding)
        {
            _startWallJumping = true;
        }
        
        if(Input.GetKeyUp("w") && !_isSliding)
        {
            _jumpCut = true;
        }
        
        if (Input.GetKeyDown("q"))
        {
            ActivateAbility();
        }
        
        if (Input.GetKeyDown("r"))
        {
            SetColor(4);
            ActivateAbility();
        }
        
        /*if (Input.GetKeyDown("q"))
        {
            ActivateAbility();
        }*/
        if (gravitySign > 0)
        {
            _grounded = Physics2D.OverlapBox(floorCheck.position, new Vector2(1f, .1f), 0, floorLayer);
        }
        else
        {
            _grounded = Physics2D.OverlapBox(roofCheck.position, new Vector2(1f, .1f), 0, floorLayer);
        }
        
        _isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer);

        if (_grounded)
        {
            _canGravity = true;
        }
        jumpBufferCounter -= Time.deltaTime;
        abilityCooldownCounter += Time.deltaTime;
        gravitySign = (_rb.gravityScale / Mathf.Abs(_rb.gravityScale));

        Flip();
    }

    public void ActivateJump()
    {
        jumpBufferCounter = jumpBuffer;
    }

    public void ActivateAbility()
    {
        if (abilityCooldownCounter > abilityCooldown)
        {
            if (_canDash && _activeColor == 2)
            {
                _startDash = true;
                abilityCooldownCounter = 0;
            }
            else if (_canRoll && _grounded && _activeColor == 3)
            {
                _startRoll = true;
                abilityCooldownCounter = 0;
            }else if (_activeColor == 4)
            {
                Debug.Log("Se ha pulsado para ejecutar gravedad");
                _startGravity = true;
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
    
    private IEnumerator Gravity()
    {
        Debug.Log("Se ejecuta gravedad");
        gravityScale = -gravityScale;
        yield return new WaitForSeconds(1);
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
        }
        PreserveMomentum();
    }

    private IEnumerator WallJump()
    {
        _isWallJumping = true;
        Debug.Log("WallJump");
        doubleJump--;
        gravityScale = 0;
        _rb.AddForce(new Vector2(wallJumpForce.x * -(transform.localScale.x), wallJumpForce.y), ForceMode2D.Impulse);
        yield return new WaitForSeconds(wallJumpDuration);
        gravityScale = 10;
        _isWallJumping = false;
    }
    
    private IEnumerator Dash()
    {
            _rb.totalForce = Vector2.zero;
            _isDashing = true;
            _tr.emitting = true;
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(inputX, inputY * gravitySign).normalized * dashForce;
            yield return new WaitForSeconds(dashingTime);
            if (!_grounded)
            {
                _canDash = false;
            }

            _tr.emitting = false;
            _isDashing = false;
            
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Roll()
    {
        _rb.totalForce = Vector2.zero;
        _isRolling = true;
        _rb.velocity = new Vector2(_horizontalInput,0).normalized * rollForce;
        GetComponent<CapsuleCollider2D>().size /= 2;
        yield return new WaitForSeconds(rollTime);
        GetComponent<CapsuleCollider2D>().size *= 2;
        _isRolling = false;
    }
    
    void PreserveMomentum()
    {
        _rb.velocity = new Vector2(_lastVelocity.x, _rb.velocity.y);
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

    void OnTriggerEnter2D(Collider2D collision){
        
        //SI TOCA EL SUELO LE DECIMOS QUE MANTENGA EL MOMENTUM, QUE NO SE FRENE AL CAER, TAMBIEN REINICIAMOS VARIABLES DE HABILIDADES
        if(collision.CompareTag("Floor"))
        {
            _jumpCut = false;
            airMove = false;
            PreserveMomentum();
            doubleJump = 2;
            _canDash = true;
            coyoteTimeCounter = coyoteTime;
            _canGravity = true;
        }
        if(collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("Main");
        }
    }
    
    public void SetColor(int color)
    {
        _activeColor = color;
    }
}
