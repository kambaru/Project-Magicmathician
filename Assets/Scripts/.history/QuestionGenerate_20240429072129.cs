using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class QuestionGenerate : MonoBehaviour
{
    public TMP_InputField answerText;
    public TMP_Text questionText;
    public TMP_Text messageText;
    public PlayerController playerController;
    public Enemy enemy;
    private int correctAnswer;
    
    public GameAI gameAI;
    private int currentTopic;

    public int[] buffs;

    private string filePath;
    
    //finds the save file path
    void Start(){
        filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("File path in question generator: " + filePath);
    }

    //generates the questions
    public void GenerateQuestion()
    {
        gameAI.LoadProgress();

        currentTopic = gameAI.GetWeakestTopic();

        int level = gameAI.GetTopicLevel(currentTopic);

        switch (currentTopic)
        {
            case 0:
                GenerateAdditionQuestion(level);
                break;
            case 1:
                GenerateSubtractionQuestion(level);
                break;
            case 2:
                GenerateMultiplicationQuestion(level);
                break;
            case 3:
                GenerateDivisionQuestion(level);
                break;
        }
    }

    //gets the buffs that are pruchased by the player
    public int[] GetBuffs(){
        gameAI.LoadProgress();
        buffs = gameAI.GetBuffs();
        return buffs;
    }

    //changes the text on screen
    public void setMessage(string message)
    {
        messageText.text = message;
        
    }

    /*
     * generating  questions based on level for each topic
     * 
     */
    private void GenerateAdditionQuestion(int level)
    {
        // level 0: add numbers between 0 and 10
        int min = 0;
        int max = 10;
        // level 1: add numbers between 0 and 20
        if (level == 1)
        {
            max = 20;
        }
        // level 2: add numbers between -50 and 50
        else if (level == 2)
        {
            min = -50;
            max = 50;
        }
        // level 3: add numbers between -100 and 100
        else if (level == 3)
        {
            min = -100;
            max = 100;
        }
        int num1 = Random.Range(min, max+1);
        int num2 = Random.Range(min, max+1);
        int result = num1 + num2;
        questionText.text = $"{num1} + {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateSubtractionQuestion(int level)
    {
        // level 0: subtract numbers between 0 and 10, and no negative results
        bool allowNegative = false;
        int min = 0;
        int max = 10;
        // level 1: subtract numbers between 0 and 20, and no negative results
        if (level == 1)
        {
            max = 20;
        }
        // level 2: subtract numbers between -50 and 50, with negative results
        else if (level == 2)
        {
            allowNegative = true;
            min = -50;
            max = 50;
        }
        // level 3: subtract numbers between -100 and 100, with negative results
        else if (level == 3)
        {
            allowNegative = true;
            min = -100;
            max = 100;
        }
        int num1 = Random.Range(min, max+1);
        int num2 = Random.Range(min, max+1);

        if (!allowNegative && num2 > num1)
        {
            int temp = num1;
            num1 = num2;
            num2 = temp;
        }

        int result = num1 - num2;
        questionText.text = $"{num1} - {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateMultiplicationQuestion(int level)
    {
        // level 0: multiply numbers between 0 and 5
        int min = 0;
        int max = 5;   
        // level 1: multiply numbers between 0 and 10
        if (level == 1)
        {
            max = 10;
        }
        // level 2: multiply numbers between 0 and 12
        else if (level == 2){
            max = 12;
        }
        // level 3: multiply numbers between -12 and 12
        else if (level == 3){
            min = -12;
            max = 12;
        }
        int num1 = Random.Range(min, max+1);
        int num2 = Random.Range(min, max+1);
        int result = num1 * num2;
        questionText.text = $"{num1} x {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateDivisionQuestion(int level)
    {
        // level 0: divide numbeers where max number is 9
        int min = 1;
        int max = 3;
        // level 1: divide numbers where max number is 25
        if (level == 1){
            max = 5;
        }
        // level 2: divide numbers where max number is 100
        else if (level == 2){
            max = 10;
        }
        // level 3: divide numbers where max number is 144
        else if (level == 3){
            max = 12;
        }

        int num1 = Random.Range(min, max+1);
        int num2 = Random.Range(min, max+1);

        int result = num1 * num2;
        questionText.text = $"{result} ÷ {num2} = ?";
        correctAnswer = num1;
    }

    // the enemy attacks after every action
    public void EnemyAttack(){
        playerController.TakeDamage(enemy.getAttackPower());
    }
    
    //submitting an answer checks if the player's input is correct and updates the correct amount of answers in GameAi
    public void SubmitAnswer()
    {
        
        string playerAnswer = answerText.text.Trim();

        if (string.IsNullOrEmpty(playerAnswer))
        {
            return;
        }

        if ((playerController.GetCurrentHealth() > 0) | (!playerController.GetIsAttacking()))
        {
            
            if (playerAnswer == correctAnswer.ToString())
            {

                enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateCorrectAnswers(currentTopic);
                int enemyHpLost = enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateTotalQuestions(currentTopic); 
                gameAI.UpdateDifficulty(); 
                setMessage("Correct answer!\nYou have dealt " + enemyHpLost + " damage.\nEnemy attacked you.\nChoose your next move.");
                EnemyAttack();
            }
            else
            {
                if ((playerController.GetCurrentHealth() > 0) && (playerController.GetIsAttacking()))
                {
                    playerController.TakeDamage(10);
                }
                EnemyAttack();
                setMessage("Incorrect!\nYou have lost 10 HP.\nEnemy attacked you.\nChoose your next move.");
                gameAI.UpdateTotalQuestions(currentTopic);
                gameAI.UpdateDifficulty();
            }


            answerText.text = "";
            questionText.text = "";
            playerAnswer = "";
            gameAI.GetCorrectAnswersAndTotalQuestions();
            gameAI.SaveProgress();
        }
        
    }

    //player gains coins
    public void GainCoins(){
        gameAI.GainCoins();
    }
}