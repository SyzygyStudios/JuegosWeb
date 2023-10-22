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
    private float _horizontalInput, _horizontalMove;

    /// Variables que controlan el salto
    [Header("Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallGravityScaleMultiplier;
    [SerializeField] private float gravityScale;
    [SerializeField] private float airFrictionMultiplier;
    
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
    private Rigidbody2D _rb;
    private int _activeColor;

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
        //SI LAS CONDICIONES PARA LOS DISTINTOS MOVIMIENTOS SE CUMPLEN SE EJECUTAN//
        
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
        
        //FALL SCALE MULTIPLIER: AUMENTA LA GRAVEDAD AL CAER, PARA DAR MEJOR SENSACION//
        if(_rb.velocity.y < 0 && !_isDashing)
        {
            _rb.gravityScale = gravityScale * fallGravityScaleMultiplier;
        }
        else if(!_isDashing)
        {
            _rb.gravityScale = gravityScale;
        }
        
        //VARIABLES MANTENER EL MOMENTUM TANTO SALTANDO COMO AL CAER//
        if (_onAir)
        {
            PreserveMomentum();
        }
        _lastVelocity = _rb.velocity;
        
    }

    void Update()
    {
        
        //SE COMPTRUEBA MEDIANTE BOOLEANOS SI SE CUMPLEN LAS CONDICIONES PARA LOS DISTINTOS MOVIMIENTOS//
        
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if(_onAir)
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
        
        if (Input.GetKeyDown("q") && _canDash && _activeColor == 2)
        {
            _startDash = true;
        }
        
        if (Input.GetKeyDown("q") && _canRoll && !_onAir && _activeColor == 3)
        {
            _startRoll = true;
        }
    }

    private void Run()
    {
        if (_onAir)
        {
            _horizontalMove = _horizontalInput * airFrictionMultiplier;
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
        
        //CASO 1: REALIZA SALTO DENTRO DEL COYOTE TIME (DESDE EL SUELO) VA A PODER REALIZAR DOS SALTOS SIMULTANEOS EN EL AIRE
        if (coyoteTimeCounter > 0f)
        {
            doubleJump--;
            Debug.Log("Voy a saltar");
            _rb.AddForce(Vector2.up * (jumpForce - _rb.velocity.y), ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
        //CASO 2: O BIEN SE HA DEJADO CAER Y HA PASADO EL COYOTE TIME, O YA HA SALTADO 1 VEZ, SEA CUAL SEA REALIZARÁ SOLO UN SALTO
        else if (doubleJump <= 2)
        {
            if (doubleJump == 1 && _activeColor != 1) return;
            doubleJump = 0;
            _rb.totalForce = Vector2.zero;
            _rb.AddForce(Vector2.up * (jumpForce * 1.1f - _rb.velocity.y),
                ForceMode2D.Impulse);
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
        }
    }
    
    private IEnumerator Dash()
    {
            _tr.emitting = true;
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");
            _isDashing = true;
            _rb.gravityScale = 0;
            _rb.totalForce = new Vector2(0, 0);
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
        _isRolling = true;
        _rb.totalForce = new Vector2(0, 0);
        _rb.velocity = new Vector2(_horizontalInput,0).normalized * rollForce;
        GetComponent<CapsuleCollider2D>().size /= 2;
        yield return new WaitForSeconds(rollTime);
        GetComponent<CapsuleCollider2D>().size *= 2;
        _isRolling = false;
    }
    
    void PreserveMomentum()
    {
        if (_lastVelocity.y < -5)
            _rb.velocity = new Vector2(_lastVelocity.x, _rb.velocity.y);
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        
        //SI TOCA EL SUELO LE DECIMOS QUE MANTENGA EL MOMENTUM, QUE NO SE FRENE AL CAER, TAMBIEN REINICIAMOS VARIABLES DE HABILIDADES
        if(collision.CompareTag("Floor"))
        {
            PreserveMomentum();
            doubleJump = 2;
            _onAir = false;
            _canDash = true;
            coyoteTimeCounter = coyoteTime;
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
