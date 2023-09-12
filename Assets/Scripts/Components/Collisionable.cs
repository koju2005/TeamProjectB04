using System;
using System.Collections;
using System.Collections.Generic;
using Data.Functions;
using DefaultNamespace.Data;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Collisionable : PooledObject
{
    [SerializeField] private CollisionInteraction[] action;

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var val in action)
        {
            val.EnterCollsion(gameObject, other.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        foreach (var val in action)
        {
            val.ExitCollsion(gameObject,other.gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var val in action)
        {
            val.EnterCollsion(gameObject,other.gameObject);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (var val in action)
        {
            val.ExitCollsion(gameObject,other.gameObject);
        }
    }
}
