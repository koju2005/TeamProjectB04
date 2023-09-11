using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PaddleLength : MonoBehaviour
{
    Health _health;
    float maxhealth;
    float length;
    private float point;

    public void Awake()
    {
        _health = GetComponent<Health>();

        length = transform.localScale.x;
        maxhealth = _health.MaxHealth;
        point = length / maxhealth;

        _health.OnHealthChanged += LengthChange;
    }


    private void LengthChange()
    {
        transform.localScale = new Vector2(point * _health.GetHealth(), transform.localScale.y);
    }

}
