using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;

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
    private bool _jumpCut;
    
    [Space(10)]
    [SerializeField] private float coyoteTime;
    [SerializeField] private float coyoteTimeCounter;
    private TrailRenderer _tr;
    
    [Space(10)]
    [SerializeField] private float jumpBuffer;
    [SerializeField] private float jumpBufferCounter;
    [SerializeField] private int doubleJump;
    private Vector2 _lastVelocity;
    
    
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
    private bool _onAir;
    private bool airMove;
    private Rigidbody2D _rb;
    [SerializeField] private int _activeColor;

    void Start()
    {
        _lastVelocity = new Vector2();
        _tr = GetComponent<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _onAir = false;
        _canDash = true;
        _isDashing = false;
        _startDash = false;
        _canRoll = true;
        _isRolling = false;
        _startRoll = false;
        doubleJump = 2;
    }

    void FixedUpdate()
    {
        
        if(_horizontalInput!=0 && !_isDashing && !_isRolling)
        {
            Run();
        }

        if(jumpBufferCounter>0f && !_isDashing && !_isRolling && doubleJump>0)
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
        
        //SI LAS CONDICIONES PARA LOS DISTINTOS MOVIMIENTOS SE CUMPLEN SE EJECUTAN//
        if(_rb.velocity.y < 0 && !_isDashing)
        {
            _rb.gravityScale = gravityScale * fallGravityMultiplier;
        }
        else if(!_isDashing)
        {
            _rb.gravityScale = gravityScale;
        }
        if(_jumpCut && _rb.velocity.y>0)
        {
            _rb.velocity -= (new Vector2(0, _rb.velocity.y/jumpCutGravityMultiplier));
        }
        //if(_jumpCut && _onAir)
        //{
        //    _rb.gravityScale = gravityScale* jumpCutGravityMultiplier;
        //}else if(!_isDashing)
        //{
        //    _rb.gravityScale = gravityScale;
        //} 
        
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

        if(_onAir)
        {
            coyoteTimeCounter-= Time.deltaTime;
        }

        if(Input.GetKeyDown("w"))
        {
            ActivateJump();
        }
        if(Input.GetKeyUp("w"))
        {
            _jumpCut = true;
        }
        
        if (Input.GetKeyDown("q"))
        {
            ActivateAbility();
        }
        
        /*if (Input.GetKeyDown("q"))
        {
            ActivateAbility();
        }*/
        
        jumpBufferCounter -= Time.deltaTime;
    }

    public void ActivateJump()
    {
        jumpBufferCounter = jumpBuffer;
    }

    public void ActivateAbility()
    {
        if (_canDash && _activeColor == 2)
        {
            _startDash = true;
        }else if (_canRoll && !_onAir && _activeColor == 3)
        {
            _startRoll = true;
        }
    }
    
    private void Run()
    {
        if (_onAir)
        {
            _horizontalMove = _horizontalInput * airFrictionMultiplier;
            if (_rb.velocity.x < 0 && _horizontalMove > 0 || _rb.velocity.x > 0 && _horizontalMove < 0 || airMove)
            {
                _horizontalMove = _horizontalMove / 2;
                airMove = true;
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
            _rb.AddForce(Vector2.up * (jumpForce), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        else if (doubleJump <= 2)
        {
            if (doubleJump == 1 && _activeColor != 1) return;
            _jumpCut = false;
            doubleJump = 0;
            _rb.AddForce(Vector2.up * (jumpForce - _rb.velocity.y),
                ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        PreserveMomentum();
    }
    
    private IEnumerator Dash()
    {
            _rb.totalForce = Vector2.zero;
            _isDashing = true;
            _tr.emitting = true;
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");
            _rb.gravityScale = 0f;
            _rb.velocity = new Vector2(inputX,inputY).normalized * dashForce;
            yield return new WaitForSeconds(dashingTime);
            _rb.gravityScale = gravityScale;
            if (_onAir)
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
    
    
    
    void OnTriggerEnter2D(Collider2D collision){
        
        //SI TOCA EL SUELO LE DECIMOS QUE MANTENGA EL MOMENTUM, QUE NO SE FRENE AL CAER, TAMBIEN REINICIAMOS VARIABLES DE HABILIDADES
        if(collision.CompareTag("Floor"))
        {
            _rb.gravityScale = gravityScale;
            PreserveMomentum();
            doubleJump = 2;
            airMove = false;
            _onAir = false;
            _canDash = true;
            coyoteTimeCounter = coyoteTime;
        }
        if(collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("Main");
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Floor"))
        {
            _onAir = true;
        }
    }

    public void SetColor(int color)
    {
        _activeColor = color;
    }
}
