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
    public int speed;
    private Rigidbody2D rigid;

    private void Awake()
    {
        speed = 4;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rigid.velocity = accelate.normalized * speed;
    }

    private void OnEnable()
    {
        rigid.velocity = accelate.normalized * speed;
    }


    void Update()
    {
        if (rigid.velocity != Vector2.zero)
        {
            rigid.velocity = rigid.velocity.normalized * speed;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (rigid.velocity.y < speed * 0.1 && rigid.velocity.y > speed * (-0.1))
        {
            if (rigid.velocity.y <= 0)
            {
                rigid.velocity += new Vector2(0, -0.3f) * speed;
            }
            if (rigid.velocity.y > 0)
            {
                rigid.velocity += new Vector2(0, 0.3f) * speed;
            }
        }
    }
}
