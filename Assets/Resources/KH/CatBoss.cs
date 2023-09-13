using DefaultNamespace;
using DefaultNamespace.Common;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CatBoss : Shooter
{
    [SerializeField] private AudioClip sfxBGM;
    [SerializeField] private DialogTyper _dialogTyper;
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
                if (PatternHealth.GetHealth() == 4)
                { _dialogTyper.Enqueue("야아아아아아옹");
                    sound();
                }
            }
            transform.position += new Vector3(MoveSpeed, 0, 0);
        }
        
    }

    void sound()
    {
        GameManager.Instance.PlaySFX(sfxBGM, new Vector3(0, 0, 0));

    }
}
