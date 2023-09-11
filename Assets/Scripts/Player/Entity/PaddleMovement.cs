using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleMovement : MonoBehaviour
{
    private PaddleController _controller;

    private Rigidbody2D _Rb;
    private Vector2 _direction = Vector2.zero;
    private Vector2 _position = Vector2.zero;
    public float _speed = 5;

    private void Awake()
    {
        _controller = GetComponent<PaddleController>();
        _Rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _controller.OnKeyMoveEvent += KeyMove;
        _controller.OnDragMoveEvent += DragMove;
    }

    private void KeyMove(Vector2 direction)
    {
        _direction = direction;
    }

    private void DragMove(Vector2 mousePos)
    {
        _position = mousePos;
    }

    void FixedUpdate()
    {
        KeyMovement();
        DragMovement();
    }

    private void KeyMovement()
    {
        _Rb.velocity = _direction * _speed;
    }

    private void DragMovement()
    {
        if (Input.GetMouseButton(0))
        {
            if (transform.position.x > _position.x - 0.1 && transform.position.x < _position.x + 0.1)
            {
                _direction = Vector2.zero;
            }
            else if (transform.position.x < _position.x)
            {
                _direction = new Vector2(1, 0);
            }
            else if (transform.position.x > _position.x)
            {
                _direction = new Vector2(-1, 0);
            }
        }

        _Rb.velocity = _direction * _speed;
    }

}