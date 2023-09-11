using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    
    [SerializeField]private  Image progressBar;
    private Health health;
    private void Awake()
    {
        health = GetComponentInParent<Health>();
        health.OnHealthChanged += SetHealthUI;
        progressBar.fillAmount = 1;
    }

    private void SetHealthUI()
    {
        progressBar.fillAmount = (float)health.GetHealthRate();
    }
}
