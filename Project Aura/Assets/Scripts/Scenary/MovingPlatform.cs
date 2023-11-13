using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float horizontalMovement;
    [SerializeField] private float speed;
    private Vector3 _currentPos;
    private float _rightFinalPos;
    private float _leftFinalPos;
    private Rigidbody2D _rb;
    private bool _movingRight;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _movingRight = true;
        _currentPos = transform.position;
        _rightFinalPos = _currentPos.x + horizontalMovement;
        _leftFinalPos = _currentPos.x - horizontalMovement;
    }
    void FixedUpdate()
    {
        _currentPos = transform.position;
        Debug.Log(_movingRight);
        if (_movingRight)
        {
            _rb.velocity = new Vector2(speed, 0f);
        }else if (!_movingRight)
        {
            _rb.velocity = new Vector2(-speed, 0f);
        }

        if (_currentPos.x >= _rightFinalPos)
        {
            Debug.Log("Llegamos");
            _movingRight = false;
        }
        else if (_currentPos.x <= _leftFinalPos)
        {
            Debug.Log("Llegamos");
            _movingRight = true;
        }
    }
}
