using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public event Action<Vector2> OnKeyMoveEvent;
    public event Action<Vector2> OnDragMoveEvent;

    public void CallKeyMoveEvent(Vector2 direction)
    {
        OnKeyMoveEvent?.Invoke(direction);
    }

    public void CallDragMoveEvent(Vector2 mousePos)
    {
        OnDragMoveEvent?.Invoke(mousePos);
    }
}


