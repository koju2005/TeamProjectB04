using DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CatBoss : Shooter
{
    public float MoveSpeed;
    private Health PatternHealth;
    private void Start()
    {
        PatternHealth = GetComponent<Health>();
        PatternHealth.OnHealthChanged += BossPattern;
        StartShootOperation();
    }
    private void Update()
    {
        BossPattern();
    }

    private void BossPattern()
    {
        
        if (PatternHealth.GetHealth() < 5)
        {
            if (transform.position.x > 2.5 || transform.position.x < -2.5)
            {
                MoveSpeed = -MoveSpeed;
            }
            transform.position += new Vector3(MoveSpeed, 0, 0);
        }
        
    }
}
