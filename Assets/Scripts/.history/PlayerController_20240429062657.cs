using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;

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
    public int[] buffs;
    public GameObject questionUI;
    public GameObject messageUI;
    public TMP_Text hpText;
    public TMP_Text manaText;
    public QuestionGenerate questionGenerator;
    public Enemy enemy;
    public float responseTime;
    private bool isAttacking = false;
    public bool answerSubmitted = false;

    public bool buffAdded = false;
    string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/SaveData.json";
        UnityEngine.Debug.Log("File path in player controller: " + filePath);
        currentHealth = maxHealth;
        currentMana = maxMana;
        questionGenerator.setMessage("Choose your next move!");
             
        messageUI.SetActive(true);
        
        healthBar.SetHealthBar(maxHealth);
        manaBar.SetManaBar(maxMana);
        UnityEngine.Debug.Log("Start Function: Player health: " + currentHealth);
    }

    //Attack to damage the enemy
    public void Attack()
    {
        
        
        responseTime = 0f;
        UnityEngine.Debug.Log("Attack Function Started.");
        if (!isAttacking)
        {
            questionUI.SetActive(true);
            questionGenerator.GenerateQuestion();

            StartCoroutine(ResponseTimeStopWatch());
            StartCoroutine(AttackCoroutine());
        }
    }

    // Coroutine to handle the attack
    private IEnumerator AttackCoroutine()
    {
        
        isAttacking = true;

        while (!answerSubmitted)
        {
            yield return null;
        }

        UnityEngine.Debug.Log("Answer submitted, checking answer...");

        questionGenerator.SubmitAnswer();

        isAttacking = false;
        answerSubmitted = false;
        questionUI.SetActive(false);

    }
    
    //Coroutine to handle heal 
    private IEnumerator HealRoutine()
    {
  
        questionUI.SetActive(false);
        if ((currentMana-10) < 0 ){
            questionGenerator.setMessage("Your mana is empty.\nChoose another option!");
            isAttacking = false;
        }
        else
        {
            if (currentHealth < maxHealth)
                {
                    int healing = maxHealth - currentHealth;
                    if (healing < 10)
                    {
                        Heal(healing);
                        }
                    else
                    {
                        Heal(10);
                        }
                }
            else
            {
                questionGenerator.setMessage("Your health is full.\nChoose another option!");
                isAttacking = false;
            }
        }


        yield return new WaitForSeconds(2f);
    }

    //
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }

    public void setManaText(){
        manaText.text = GetCurrentMana().ToString();
    }

    public void setHpText(){
        hpText.text = GetCurrentHealth().ToString();
    }

    private void Update()
    {
        UnityEngine.Debug.Log("Update");
        if (!buffAdded)
        {
            buffs = questionGenerator.GetBuffs();
            baseAttackDamage += buffs[0];
            maxHealth += buffs[1];
            maxMana += buffs[2];
            currentHealth = maxHealth;
            currentMana = maxMana;
            healthBar.SetHealthBar(maxHealth);
            manaBar.SetManaBar(maxMana);
            buffAdded = true;
        }
        setHpText();
        setManaText();

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
            OnPlayerDestroyed();
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
        questionGenerator.EnemyAttack();
        questionGenerator.setMessage("You have healed.\nEnemy has attacked you.\nSelect your next move.");
    }

    public void OnEnemyDestroyed()
    {
        StopAllCoroutines();
        isAttacking = false;
        answerSubmitted = false;
        questionUI.SetActive(false);
        questionGenerator.GainCoins();

        SceneManager.LoadSceneAsync("ExplorationMenu");
    }

    public void OnPlayerDestroyed()
    {
        StopAllCoroutines();
        isAttacking = false;
        answerSubmitted = false;
        questionUI.SetActive(false);

        SceneManager.LoadSceneAsync("ResultsScene");
    }

    public void OnHealButton()
    {
        StartCoroutine(HealRoutine());
    }

    public void OnAttackButton()
    {
        Attack();
        questionGenerator.setMessage("ENTER to submit.");
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
