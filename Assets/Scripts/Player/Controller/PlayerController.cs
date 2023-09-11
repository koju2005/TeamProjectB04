using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : PaddleController
{
    private Vector2 _direction;
    private Vector2 _position;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }


    void OnKeyboardMove(InputValue vlaue)
    {
        _direction = vlaue.Get<Vector2>().normalized;
        //Debug.Log("dddd");
        CallKeyMoveEvent(_direction);
    }

    void OnMousePosition(InputValue value)
    {
        Vector2 ScreenPos = value.Get<Vector2>();
        _position = _camera.ScreenToWorldPoint(ScreenPos);
        
        CallDragMoveEvent(_position);
    }
}


