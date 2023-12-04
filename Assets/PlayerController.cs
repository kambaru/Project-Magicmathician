using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;
    public float attackRange = 2f;
    public int baseAttackDamage = 10;
    public float maxResponseTime = 3f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    public QuestionGenerate questionGenerator;
    public LayerMask enemyLayer;
    private float responseTime;
    private bool isAttacking;
    private bool answerSubmitted = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetHealthBar(maxHealth);
        Debug.Log("Start Function: Player health: " + currentHealth);
        Attack();
    }

    //Attack to damage the enemy
    public void Attack()
    {
        Debug.Log("Attack Function Started");
        if (!isAttacking)
        {
            questionGenerator.GenerateQuestion();
            StartCoroutine(ResponseTimeStopWatch());
            StartCoroutine(AttackCoroutine());
        }
    }

    // Coroutine to handle the attack
    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        responseTime = 0f;

        yield return new WaitForSecondsRealtime(maxResponseTime);

        Debug.Log("CoRoutine Function Started");

        if(!answerSubmitted)
        {
            yield break;
        }
        
        if (isAttacking)
        {
            isAttacking = false;
        	Attack();
        }

        if (responseTime <= maxResponseTime)
        {
            // Check if the player's answer is correct
            if (questionGenerator.CheckAnswer())
            {
                Debug.Log("Correct answer: attacking enemy");
                float damageMultiplier = 1f - (responseTime / maxResponseTime);
                int attackDamage = Mathf.RoundToInt(baseAttackDamage * damageMultiplier);

                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

                foreach (Collider2D enemy in enemies)
                {
                    Enemy enemyController = enemy.GetComponent<Enemy>();
                    if (enemyController != null)
                    {
                        enemyController.TakeDamage(attackDamage);
                    }
                }
            }
            else
            {
                Debug.Log("Wrong answer");
                TakeDamage(10);
            }
        }

        // Reset variables
        isAttacking = false;

        Attack();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("Update");
        if (isAttacking)
        {
            responseTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                questionGenerator.SubmitAnswer();
                answerSubmitted = true;

            }
        }
    }

    private IEnumerator ResponseTimeStopWatch()
    {
        isAttacking = true;
        responseTime = 0f;

        while ((isAttacking) && (answerSubmitted))
        {
            responseTime += Time.deltaTime;
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealthBar(currentHealth);
        Debug.Log("Player health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool GetIsAttacking()
    {
        return isAttacking;
    }

    public bool GetAnswerSubmitted()
    {
        return answerSubmitted;
    }

    public bool SetAnswerSubmitted(bool answerSubmitted)
    {
        this.answerSubmitted = answerSubmitted;
        return answerSubmitted;
    }
}