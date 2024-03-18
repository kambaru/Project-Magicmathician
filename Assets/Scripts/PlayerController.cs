using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public HealthBar healthBar;
    public ManaBar manaBar;
    public float attackRange = 2f;
    public int baseAttackDamage = 10;
    public float maxResponseTime = 3f;
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] int maxMana = 100;
    [SerializeField] int currentMana;
    public QuestionGenerate questionGenerator;
    public LayerMask enemyLayer;
    //public Enemy enemy;
    public float responseTime;
    private bool isAttacking;
    public bool answerSubmitted = false;

    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        healthBar.SetHealthBar(maxHealth);
        manaBar.SetManaBar(maxMana);
        UnityEngine.Debug.Log("Start Function: Player health: " + currentHealth);
        Attack();
    }

    //Attack to damage the enemy
    public void Attack()
    {
        UnityEngine.Debug.Log("Attack Function Started");
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

        // This while loop will hold the coroutine here until the player submits an answer
        while (!answerSubmitted)
        {
            yield return null; // Ensures the coroutine will resume on the next frame
        }

        // At this point the coroutine will only advance past the while loop when the player has submitted an answer
        UnityEngine.Debug.Log("Answer submitted, checking answer...");

        questionGenerator.SubmitAnswer();

        // Reset variables
        isAttacking = false;
        answerSubmitted = false;

        // Starts the next attack
        Attack();
    }
    
    //Coroutine to handle heal 
    private IEnumerator HealRoutine()
    {
        Heal(10);

        yield return new WaitForSeconds(2f);
    }
    // Update is called once per frame
    private void Update()
    {
        UnityEngine.Debug.Log("Update");
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

        while (!answerSubmitted)
        {
            yield return null;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealthBar(currentHealth);
        UnityEngine.Debug.Log("Player health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int heal)
    {
        currentHealth += 10;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        currentMana -= 10;
        healthBar.SetHealthBar(currentHealth);
        manaBar.SetManaBar(currentMana);
        UnityEngine.Debug.Log("Player health: " + currentHealth);
    }

    public void OnHealButton()
    {
        StartCoroutine(HealRoutine());
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
