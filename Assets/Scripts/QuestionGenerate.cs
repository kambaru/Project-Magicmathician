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
    
    void Start(){
        filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("File path in question generator: " + filePath);
    }
    public void GenerateQuestion()
    {
        gameAI.LoadProgress();

        currentTopic = gameAI.GetWeakestTopic();

        int level = gameAI.getTopicLevel(currentTopic);

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

    public int[] GetBuffs(){
        gameAI.LoadProgress();
        buffs = gameAI.GetBuffs();
        return buffs;
    }

    public void setMessage(string message)
    {
        messageText.text = message;
        
    }

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

    public void EnemyAttack(){
        playerController.TakeDamage(enemy.getAttackPower());
    }
    
    public void SubmitAnswer()
    {
        
        string playerAnswer = answerText.text.Trim();

        if (string.IsNullOrEmpty(playerAnswer))
        {
            return;
        }
        Debug.Log("The current question is " + questionText.text + " " + correctAnswer + " is the actual answer, " + playerAnswer + " is the correct answer. The current topic is: " + currentTopic);
        //int intAnswer = ConvertToInt(playerAnswer);

        if ((playerController.GetCurrentHealth() > 0) | (!playerController.GetIsAttacking()))
        {
            

            if (playerAnswer == correctAnswer.ToString())
            {
                Debug.Log(playerAnswer + " is the correct answer");
                //stores this as 1 correct in the player data
                Debug.Log("Response Time: " + playerController.responseTime);
                enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateCorrectAnswers(currentTopic);
                int enemyHpLost = enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateTotalQuestions(currentTopic); // Add this line
                gameAI.UpdateDifficulty(); // Add this line
                setMessage("Correct answer!\nYou have dealt " + enemyHpLost + " damage.\nEnemy attacked you.\nChoose your next move.");
                EnemyAttack();
            }
            else
            {
                Debug.Log("Wrong Answer: " + playerAnswer + " is not the same as " + correctAnswer.ToString() +" Question Generator");
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
            //GenerateQuestion();
        }
        else
        {
            Debug.Log("Player is dead");
        }
        
    }

    public void GainCoins(){
        gameAI.GainCoins();
    }
}