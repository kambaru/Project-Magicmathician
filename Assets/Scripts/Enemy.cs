using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public HealthBar hpBar;
    public int timeDamage;
    [SerializeField] int currentHealth; 
    
    [SerializeField] int maxHealth = 100;

    private void Start()
    {
        currentHealth = maxHealth;
        hpBar.SetHealthBar(currentHealth);
    }

    private void Update()
    {
        UnityEngine.Debug.Log("Enemy Update");
        TakeDamage(0, 0f);
        hpBar.SetHealthBar(currentHealth);
    }

    public void TakeDamage(int damage, float responseTime)
    {
        UnityEngine.Debug.Log("Enemy takes " + damage + " damage. Current health: " + currentHealth);
        UnityEngine.Debug.Log("You took " + responseTime + " seconds.");
        if (responseTime < 30)
        {
            timeDamage = Mathf.RoundToInt((30 - responseTime) / (30) * (float)damage);
        }
        else {
            timeDamage = damage;
        }

        UnityEngine.Debug.Log("Your damage is " + timeDamage);
        currentHealth = currentHealth - (int)timeDamage;
        hpBar.SetHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}