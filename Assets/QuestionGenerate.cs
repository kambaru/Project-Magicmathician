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

    // Call this method to generate a new question
    public void GenerateQuestion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int result = num1 * num2;
        questionText.text = $"{num1} x {num2} = ?";
        correctAnswer = result;
        answerText.text = string.Empty;
    }

    // Call this method to check the player's answer
    public bool CheckAnswer()
    {
        string playerAnswer = answerText.text.Trim();
        int intAnswer = ConvertToInt(playerAnswer);
        return intAnswer == correctAnswer;
    }

    public void SubmitAnswer()
    {   
        string playerAnswer = answerText.text.Trim();

        if (string.IsNullOrEmpty(playerAnswer))
        {
            return;
        }
        int intAnswer = ConvertToInt(playerAnswer);

        if ((playerController.GetCurrentHealth() > 0) | (!playerController.GetIsAttacking()))
        {
            if (intAnswer == correctAnswer)
            {
                Debug.Log("Correct Answer");
                enemy.TakeDamage(20);
                //playerController.Attack();
            }
            else
            {
                Debug.Log("Wrong Answer");
                playerController.TakeDamage(10);
            }
            GenerateQuestion();
        }
        else
        {
            Debug.Log("Player is dead");
        }
        
    }

    // Utility method to convert string to integer
    private int ConvertToInt(string answer)
    {
        if (int.TryParse(answer, out int intValue))
        {
            return intValue;
        }
        else
        {
            Debug.LogError("Input string is not a valid integer");
        }
        return 0;
    }

    private void Update()
    {
        Debug.Log("Player Input: " + answerText.text);
    }
}