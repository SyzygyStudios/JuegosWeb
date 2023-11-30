using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
    [SerializeField] private int _activeColor;
    private float abilityCooldownCounter;
    private Vector2 _lastVelocity;
    private bool airMove;
    private Rigidbody2D _rb;
    private Animator animator;
    private GameMetrics _gameMetrics;
    private bool _canMove;
    private BackgroundMusicManager _backgroundMusic;
    private EffectsAudioManager _effectsAudio;
    

    /// Variables que controlan el movimiento al correr
    [Header("Run")]
    [SerializeField] private float speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
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
    [SerializeField] private int doubleJump;
    
    [Header("Wall Jump")]
    [SerializeField] private float wallSlideSpeed;
    [SerializeField] private float wallJumpDuration;
    [SerializeField] private float checkTime;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Vector2 wallJumpForce;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private bool _isWallTouch;
    private bool _startWallJumping;
    private bool _isWallJumping;
    private bool _isSliding;
    private bool _changingWall;
    
    ///  Variables que controlan el dash y el rodar
    [Header("Dash")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashingTime;
    private bool _resetDash;
    private bool _startDash;
    private bool _isDashing;
    
    [Header("Roll")]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    [SerializeField] private bool _isRolling;
    private bool _canRoll;
    private bool _startRoll;
    
    [Header ("Gravity Ability")]
    [SerializeField] private bool _startGravity;
    [SerializeField] private float gravitySign;
    
    [Header ("Bomb Jump")]
    [SerializeField] private float bombJumpVelocity;
    [SerializeField] private bool _isBombJumping;
    [SerializeField] private bool _startBombJump;
    [SerializeField] private bool landing;
    [SerializeField] private bool _lastGrounded;
    [SerializeField] private float bombJumpEndAnimatiorDuration;
    private double _rollingTime;
    private bool _touchingRoof;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private bool _isWalking;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private bool _joystickActive;
    private bool _resetGravity;
    private bool _hoverColor;

    void Start()
    {
        InitializeVariables();
    }

    void FixedUpdate()
    {
        if (_canMove)
        {
            if (Physics2D.OverlapBox(roofCheck.position, new Vector2(0.5f, 1f), 0, floorLayer))
            {
                _touchingRoof = true;
                Debug.Log("Estoy tocando roof");
            }
            else if (!Physics2D.OverlapBox(roofCheck.position, new Vector2(0.5f, .1f), 0, floorLayer))
            {
                _touchingRoof = false;
            }

            //CORRER

            if (_horizontalInput != 0 && !_isBombJumping)
            {
                Run();
                if (_grounded)
                {
                    _effectsAudio.PlayWalk();
                }

                if (!_grounded)
                {
                    _effectsAudio.StopWalk();
                }
            }
            else
            {
                _effectsAudio.StopWalk();
            }

            //SALTAR

            if (jumpBufferCounter > 0f)
            {
                Jump();
            }

            //DASH

            if (_startDash)
            {
                _startDash = false;
                StartCoroutine(Dash());
            }

            //RODAR

            if (_startRoll)
            {
                _startRoll = false;
                Roll();
            }

            if (_isRolling)
            {
                _rollingTime += Time.deltaTime;
                if (_rollingTime > rollTime && !_touchingRoof)
                {
                    _boxCollider.isTrigger = false;
                    _isRolling = false;
                    _rollingTime = 0;
                }
            }

            //GRAVEDAD

            if (_startGravity)
            {
                _startGravity = false;
                StartCoroutine(Gravity());
            }

            //SALTO BOMBA

            if (_startBombJump)
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

            if (_isSliding && gravityScale > 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
            }
            else if (_isSliding && gravityScale < 0)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, wallSlideSpeed);
            }

            //CONTROL DE GRAVEDAD AL CAER
            if (_rb.velocity.y < 0 && !_grounded)
            {
                _rb.gravityScale = gravityScale * fallGravityMultiplier;
            }
            else
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

    }

    void Update()
    {
        if (_canMove)
        {
            if (CanRun())
            {
                if (!_joystickActive)
                {
                    _horizontalInput = Input.GetAxisRaw("Horizontal");
                }else if (_joystickActive)
                {
                    _horizontalInput = _joystick.Horizontal;
                }
            }

            if (!_grounded)
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (!_grounded && _isWallTouch && _horizontalInput != 0 && _activeColor == 6)
            {
                _isSliding = true;
            }
            else
            {
                _isSliding = false;
            }

            if (Input.GetKeyDown("space") && CanJump())
            {
                ActivateJump();
            }

            if (Input.GetKeyDown("space") && CanWallJump())
            {
                ActivateWallJump();
            }

            if (Input.GetKeyDown("e") && !_hoverColor)
            {
                ActivateAbility();
            }

            if (Input.GetKeyUp("space") && !_grounded)
            {
                ActivateJumpCut();
            }


            GroundCheck();
            WallCheck();
            
            animator.SetFloat("xVelocity", Mathf.Abs(_rb.velocity.x));
            animator.SetFloat("yVelocity", _rb.velocity.y);
            animator.SetBool("isRolling", _isRolling);

            jumpBufferCounter -= Time.deltaTime;
            abilityCooldownCounter += Time.deltaTime;
            gravitySign = (_rb.gravityScale / Mathf.Abs(_rb.gravityScale));
            _lastVelocity = _rb.velocity;
            Flip();
        }
    }
    
    private void InitializeVariables()
    {
        _gameMetrics = FindObjectOfType<GameMetrics>();
        _hoverColor = false;
        animator = GetComponent<Animator>();
        _joystickActive = false;
        FindObjectOfType<PhoneController>().SetPlayer();
        if (FindObjectOfType<PhoneController>().GetActive())
        {
            FindObjectOfType<PhoneController>().SetJoystick();
            _joystickActive = true;
        }
        _tr = GetComponent<TrailRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _canMove = true;
        _grounded = false;
        _resetDash = true;
        _isDashing = false;
        _startDash = false;
        _canRoll = true;
        _isRolling = false;
        _startRoll = false;
        _changingWall = false;
        doubleJump = 2;
        _backgroundMusic = gameObject.GetComponentInChildren<BackgroundMusicManager>();
        _effectsAudio = gameObject.GetComponentInChildren<EffectsAudioManager>();
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            DisableMovement();
        }
    }

    public void SetJoystick(Joystick j, bool b)
    {
        if (b)
        {
            _joystickActive = true;
            _joystick = j;
        }
        else
        {
            _joystickActive = false;
        }
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
            if(_grounded) animator.SetTrigger("TakeOf");
            _rb.AddForce(Vector2.up * (jumpForce * gravitySign), ForceMode2D.Impulse);
            _effectsAudio.PlayJump();
            _gameMetrics.AddJump();
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            PreserveMomentum();
        }
        else if (doubleJump <= 2)
        {
            Debug.Log("Doble salto");
            if (_activeColor != 1) return;
            _jumpCut = false;
            doubleJump = 0;
            if(_grounded) animator.SetTrigger("TakeOf");
            else if(!_grounded) animator.SetTrigger("DoubleJump");
            _rb.AddForce(Vector2.up * ((jumpForce - _rb.velocity.y) * gravitySign),
                ForceMode2D.Impulse);
            _effectsAudio.PlayJump();
            _gameMetrics.AddJump();
            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;
            PreserveMomentum();
        }
    }
    
    private IEnumerator Gravity()
    {
        Debug.Log("Se ejecuta gravedad");
        _resetGravity = false;
        gravityScale = -gravityScale;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
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
        animator.SetBool("isDashing", true);
        _tr.emitting = true;
        var inputX = transform.localScale.x;
        _rb.velocity = Vector2.zero;
        _rb.velocity = new Vector2(inputX, 0).normalized * dashForce;
        yield return new WaitForSeconds(dashingTime);
        if (!_grounded)
        {
            Debug.Log("ESTOY EN EL AIRE");
            _resetDash = false;
        }

        _tr.emitting = false;
        gravityScale = prevGravityScale;
        _isDashing = false;
        animator.SetBool("isDashing", false);
            
    }
    
    private void Roll()
    {
        _isRolling = true;
        _rb.totalForce = Vector2.zero;
        _rb.velocity = new Vector2(_horizontalInput,0).normalized * rollForce;
        _boxCollider.isTrigger = true;
    }

    private void BombJump()
    {
        _isBombJumping = true;
        animator.SetBool("isBombJumping", true);
        _rb.velocity = new Vector2(0, -bombJumpVelocity * gravitySign);
    }

    private bool CanRun()
    {
        return !_isDashing && !_isRolling && !_isBombJumping;
    }

    public bool CanJump()
    {
        return (!_isDashing && !_isRolling && doubleJump>0 && !_isWallJumping && !_isBombJumping && !_isSliding);
    }
    
    private bool CanGravity()
    {
        return _activeColor == 5 && _resetGravity;
    }
    
    private bool CanWallJump()
    {
        return _isSliding;
    }
    
    private bool CanDash()
    {
        return _resetDash && _activeColor == 2;
    }
    
    private bool CanRoll()
    {
        return _canRoll && _grounded && _activeColor == 3 && !_isRolling;
    }
    
    private bool CanBombJump()
    {
        return !_grounded && _activeColor == 4;
    }
    
    private void GroundCheck()
    {
        _grounded = Physics2D.OverlapBox(floorCheck.position, new Vector2(0.5f, .1f), 0, floorLayer);

        if (!_lastGrounded && _grounded)
        {
            landing = true;
        }

        if (!_lastGrounded && _grounded)
        {
            doubleJump = 2;
            _resetDash = true;
            _jumpCut = false;
            _resetGravity = true;
            airMove = false;
            coyoteTimeCounter = coyoteTime;
        }
        
        animator.SetBool("isOnAir", !_grounded);
        _lastGrounded = _grounded;
    }

    private void WallCheck()
    {
        if (_isWallTouch && !Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer))
        {
            StartCoroutine(WallCheckChange());
        }
        else
        {
            _isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer);
        }
    }

    private IEnumerator WallCheckChange()
    {
        _changingWall = true;
        yield return new WaitForSeconds(checkTime);
        _changingWall = false;
        _isWallTouch = Physics2D.OverlapBox(wallCheck.position, new Vector2(1f, .1f), 0, wallLayer);
    }

    private IEnumerator FinishBombJump()
    {
        yield return new WaitForSeconds(bombJumpEndAnimatiorDuration);
        _isBombJumping = false;

    }
    
    public void PreserveMomentum()
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
    
    public void SetColor(int color)
    {
        int prevColor = _activeColor;
        _activeColor = color;
        
        if (_activeColor!=prevColor)
        {
            _effectsAudio.PlayPickPower();
        }

        if (_activeColor == 0)
        {
            if (gravityScale < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
            }
            gravityScale = Mathf.Abs(gravityScale);
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        
        //SI TOCA EL SUELO LE DECIMOS QUE MANTENGA EL MOMENTUM, QUE NO SE FRENE AL CAER, TAMBIEN REINICIAMOS VARIABLES DE HABILIDADES
        //if(collision.CompareTag("Floor") || collision.CompareTag("ShatteredFloor"))
        //{
        //    Debug.Log("He colisionado con "+ collision.tag);
        //    if (_isBombJumping)
        //    {
        //        _isBombJumping = false;
        //        animator.SetBool("isBombJumping", false);
        //        if(collision.CompareTag("ShatteredFloor"))
        //        {
        //            collision.gameObject.GetComponent<ShatteredFloor>().Break();
        //        }
        //    }
        //}
        if(collision.CompareTag("Door"))
        {
        }
        
        if(collision.CompareTag("Star"))
        {
            _gameMetrics.CollectStart();
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("ShatteredFloor"))
        {
            PreserveMomentum();
            Debug.Log("He colisionado con "+ collision.collider.tag);
            if (_isBombJumping)
            {
                animator.SetBool("isBombJumping", false);
                StartCoroutine(FinishBombJump());
                if(collision.collider.CompareTag("ShatteredFloor"))
                {
                    collision.gameObject.GetComponent<ShatteredFloor>().Break();
                }
            }
        }
    }
    
    public void DisableMovement()
    {
        _canMove = false;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    public void SetHoverPower(bool p0)
    {
        _hoverColor = p0;
    }
}
