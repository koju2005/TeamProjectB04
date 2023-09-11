using DefaultNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HPtext : MonoBehaviour
{
    GameObject player;
    Health health;
    TMP_Text TMPtext;
    private string hp;

    private void Awake()
    {
        player = GameManager.Instance.GetPlayer();
        health = player.GetComponent<Health>();
        TMPtext = transform.GetComponent<TMP_Text>();
        hp = "X " + health.MaxHealth.ToString();

        TMPtext.text = hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        health.OnHealthChanged += ChangeHPtext;
    }

    private void ChangeHPtext()
    {
        hp = "X " + health.GetHealth().ToString();

        TMPtext.text = hp;
    }
}
