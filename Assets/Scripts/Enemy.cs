using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar hpBar;
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
        TakeDamage(0);
        hpBar.SetHealthBar(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        UnityEngine.Debug.Log("Enemy takes " + damage + " damage. Current health: " + currentHealth);
        currentHealth = currentHealth - damage;
        hpBar.SetHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

}