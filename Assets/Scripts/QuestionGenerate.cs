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
    //takes reference from ML
    public void GenerateQuestion()
    {
        int num1 = Random.Range(1, 10);
        int num2 = Random.Range(1, 10);
        int result = num1 * num2;
        questionText.text = $"{num1} x {num2} = ?";
        correctAnswer = result;
        answerText.text = string.Empty;
    }

    public void SubmitAnswer()
    {
        
        string playerAnswer = answerText.text.Trim();

        if (string.IsNullOrEmpty(playerAnswer))
        {
            return;
        }

        //int intAnswer = ConvertToInt(playerAnswer);

        if ((playerController.GetCurrentHealth() > 0) | (!playerController.GetIsAttacking()))
        {
            if (playerAnswer == correctAnswer.ToString())
            {
                Debug.Log(playerAnswer + " is the correct answer");
                //stores this as 1 correct in the player data

                enemy.TakeDamage(playerController.baseAttackDamage, playerController.responseTime);
                playerController.Attack();
            }
            else
            {
                //stores this as 1 wrong in the player data
                
                Debug.Log("Wrong Answer: " + playerAnswer + " is not the same as " + correctAnswer.ToString() +" Question Generator");
                playerController.TakeDamage(10);
            }
            GenerateQuestion();
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