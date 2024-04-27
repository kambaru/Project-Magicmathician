using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class QuestionGenerate : MonoBehaviour
{
    public TMP_InputField answerText;
    public TMP_Text questionText;
    public PlayerController playerController;
    public Enemy enemy;
    private int correctAnswer;
    
    public GameAI gameAI;
    private int currentTopic;

    public void GenerateQuestion()
    {
        currentTopic = gameAI.GetWeakestTopic();

        switch (currentTopic)
        {
            case 0:
                GenerateAdditionQuestion(1,10);
                break;
            case 1:
                GenerateSubtractionQuestion(false);
                break;
            case 2:
                GenerateMultiplicationQuestion(0,12);
                break;
            case 3:
                GenerateDivisionQuestion();
                break;
            /*case 1:
                GenerateAdditionQuestion(1, 10);
                break;
            case 2:
                GenerateAdditionQuestion(10, 100);
                break;
            case 3:
                GenerateSubtractionQuestion(false);
                break;
            case 4:
                GenerateSubtractionQuestion(true);
                break;
            case 5:
                GenerateMultiplicationQuestion(1, 13);
                break;
            case 6:
                GenerateMultiplicationQuestion(-12, 13);
                break;
            case 7:
                GenerateMixedMultiplicationQuestion();
                break;
            case 8:
                GenerateDivisionQuestion();
                break;*/
        }
    }

    private void GenerateAdditionQuestion(int min, int max)
    {
        int num1 = Random.Range(min, max);
        int num2 = Random.Range(min, max);
        int result = num1 + num2;
        questionText.text = $"{num1} + {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateSubtractionQuestion(bool allowNegative)
    {
        int num1 = Random.Range(1, 100);
        int num2 = Random.Range(1, 100);

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

    private void GenerateMultiplicationQuestion(int min, int max)
    {
        int num1 = Random.Range(min, max);
        int num2 = Random.Range(min, max);
        int result = num1 * num2;
        questionText.text = $"{num1} x {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateMixedMultiplicationQuestion()
    {
        int num1 = Random.Range(-12, 13);
        int num2 = Random.Range(-12, 13);
        int result = num1 * num2;
        questionText.text = $"{num1} x {num2} = ?";
        correctAnswer = result;
    }

    private void GenerateDivisionQuestion()
    {
        int num1 = Random.Range(1, 13);
        int num2 = Random.Range(1, 13);
        int result = num1 * num2;
        questionText.text = $"{result} ÷ {num2} = ?";
        correctAnswer = num1;
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

                enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateCorrectAnswers(currentTopic);
                enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                gameAI.UpdateTotalQuestions(currentTopic); // Add this line
                gameAI.UpdateDifficulty(); // Add this line
            }
            else
            {
                //stores this as 1 wrong in the player data
                Debug.Log("Wrong Answer: " + playerAnswer + " is not the same as " + correctAnswer.ToString() +" Question Generator");
                if ((playerController.GetCurrentHealth() > 0) && (playerController.GetIsAttacking()))
                {
                    playerController.TakeDamage(10);
                }
                gameAI.UpdateTotalQuestions(currentTopic);
                gameAI.UpdateDifficulty();
            }

            answerText.text = "";
            questionText.text = "";
            playerAnswer = "";

            gameAI.GetCorrectAnswersAndTotalQuestions();
            //GenerateQuestion();
        }
        else
        {
            Debug.Log("Player is dead");
        }
        
    }

    private void Update()
    {
        Debug.Log("Player Input: " + answerText.text);
    }
}