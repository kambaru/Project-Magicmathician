using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float attackRange = 2f;
    public int baseAttackDamage = 10;
    public float maxResponseTime = 3f;

    public LayerMask enemyLayer;
    public TMP_Text questionText;
     public TMP_InputField answerText;

    private string correctAnswer;
    private float responseTime;
    private bool isAttacking;

    void Start()
    {
        answerText.onEndEdit.AddListener(delegate { SubmitAnswer(); });
        Attack();
    }

    //Attack to damage the enemy
    public void Attack()
    {
        if (!isAttacking)
        {
            int num1 = Random.Range(1, 10);
            int num2 = Random.Range(1, 10);
            int result = num1 * num2;

            questionText.text = $"{num1} x {num2} = ?";
            correctAnswer = result.ToString();

            answerText.text = string.Empty;

            StartCoroutine(ResponseTimeStopWatch());
        }
    }

    // Coroutine to handle the attack
    private IEnumerator AttackCoroutine()
    {
        isAttacking = true;
        responseTime = 0f;

        yield return new WaitForSecondsRealtime(maxResponseTime);

        if (isAttacking)
        {
            questionText.text = string.Empty;
            isAttacking = false;
            
        	Attack();
        }

        if (responseTime <= maxResponseTime)
        {
            // Check if the player's answer is correct
            string playerAnswer = Input.inputString.Trim();
            Debug.Log(playerAnswer);
            if (playerAnswer == correctAnswer)
            {
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
        }

        // Reset variables
        questionText.text = string.Empty;
        isAttacking = false;

        Attack();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAttacking)
        {
            responseTime += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitAnswer();
            }
        }
    }

    // Draws the attack range in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    private void SubmitAnswer()
    {
        if (responseTime <= maxResponseTime)
        {
            string playerAnswer = answerText.text.Trim();
            if (playerAnswer == correctAnswer)
            {
                Debug.Log("Player answer: " + playerAnswer + ", Correct");
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
        }
        // Reset variables
        questionText.text = string.Empty;
        isAttacking = false;
        ResponseTimeStopWatch();
        // Immediately start next question
        Attack();
    }

    private IEnumerator ResponseTimeStopWatch()
    {
        isAttacking = true;
        responseTime = 0f;

        while (isAttacking)
        {
            responseTime += Time.deltaTime;
            yield return null;
        }
    }
}