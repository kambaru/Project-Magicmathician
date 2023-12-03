using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthBar hpBar;
    [SerializeField] float health, maxHealth = 3f;
    public Transform target;

    public void TakeDamage(int damage)
    {
        health -= damage;
        hpBar.UpdateHealthBar(health, maxHealth);
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
        hpBar.UpdateHealthBar(health, maxHealth);
    }

}

