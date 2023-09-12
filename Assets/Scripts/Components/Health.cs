using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DefaultNamespace;
using DefaultNamespace.Data;
using UnityEngine.Serialization;
[RequireComponent(typeof(PolygonCollider2D))]
public class Health : MonoBehaviour
{
    public int MaxHealth; 
    public event Action OnHealthChanged;
    public event Action<string> OnDeath;
    [SerializeField]private int _health = 0;

    private PooledObject pool; 
    private void Awake()
    {
        OnDeath += GameManager.Instance.CheckDeathCount;
        pool = GetComponent<PooledObject>();
    }

    private void Start()
    {
        _health = MaxHealth;
    }

    public void AddHealth(int damage)
    {
        _health += damage;
        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
        OnHealthChanged?.Invoke();
    }

    private void Die()
    {
        OnDeath?.Invoke(tag);
        if (pool)
        {
            pool.OnRelease(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public double GetHealthRate()
    {
        return (double)_health / (double)MaxHealth;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public void HitRecoveryColor(Func<GameObject,IEnumerator> recoveryFunction,GameObject hitRecoveryTarget)
    {
        if(hitRecoveryTarget.activeSelf)
            StartCoroutine(recoveryFunction.Invoke(hitRecoveryTarget));
    }
}
