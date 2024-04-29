using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Class for the Enemy character and their behaviour
public class Enemy : MonoBehaviour
{
    public HealthBar hpBar;
    public int timeDamage;

    public int attackPower = 5;

    public PlayerController playerController;
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

    public int getAttackPower(){
        int attack = Random.Range(5, 11);
        return attack;
    }

    public int TakeDamage(int damage, float responseTime)
    {
        int expectedTime = 30;
        if (responseTime < expectedTime)
        {
            timeDamage = Mathf.RoundToInt((expectedTime - responseTime) / (expectedTime) * (float)damage);
        }
        else {
            timeDamage = 1;
        }
        currentHealth = currentHealth - (int)timeDamage;
        hpBar.SetHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            OnEnemyDestroyed();
        }
        return timeDamage;
    }

    public void OnEnemyDestroyed()
    {
        playerController.OnEnemyDestroyed();
        Destroy(gameObject);
    }

}