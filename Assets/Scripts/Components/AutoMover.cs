using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Data;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class AutoMover : PooledObject
{
    public Vector2 accelate;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigid.velocity = accelate;
    }

    private void OnEnable()
    {
        rigid.velocity = accelate;
    }
}
