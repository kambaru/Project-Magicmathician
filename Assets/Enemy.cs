using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthBar hpBar;
    [SerializeField] int health, maxHealth = 100;
    public Transform target;

    public void TakeDamage(int damage)
    {
        health -= damage;
        hpBar.SetHealthBar(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        hpBar = GetComponentInChildren<HealthBar>();
    }

    private void Start()
    {
        health = maxHealth;
        hpBar.SetHealthBar(health);
    }

}

