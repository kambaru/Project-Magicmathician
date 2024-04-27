using UnityEngine;

public class GameAI : MonoBehaviour
{
    public int[] correctAnswers = new int[4]; // [addition, subtraction, multiplication, division]
    public int[] totalQuestions = new int[4];
    public int currentLevel = 1;

    public void GetCorrectAnswersAndTotalQuestions()
    {
        string logMessage = "Correct Answers: [";
        for (int i = 0; i < correctAnswers.Length; i++)
        {
            logMessage += correctAnswers[i];
            if (i < correctAnswers.Length - 1)
                logMessage += ", ";
        }
        logMessage += "] Total Questions: [";
        for (int i = 0; i < totalQuestions.Length; i++)
        {
            logMessage += totalQuestions[i];
            if (i < totalQuestions.Length - 1)
                logMessage += ", ";
        }
        logMessage += "]";
        Debug.Log(logMessage);
    }

    public void UpdateCorrectAnswers(int topic)
    {
        correctAnswers[topic]++;
    }

    public void UpdateTotalQuestions(int topic)
    {
        totalQuestions[topic]++;
    }

    public int GetWeakestTopic()
    {
        float[] accuracy = new float[4];

        for (int i = 0; i < accuracy.Length; i++)
        {
            if (totalQuestions[i] == 0)
            {
                return i;
            }
            accuracy[i] = (float)correctAnswers[i] / totalQuestions[i];
        }

        float minAcc = accuracy[0];
        int minTopic = 0;
        for (int i = 1; i < accuracy.Length; i++)
        {
            if (accuracy[i] < minAcc)
            {
                minAcc = accuracy[i];
                minTopic = i;
            }
        }
        return minTopic;
    }

    public void UpdateDifficulty()
    {
        int totalCorrect = 0;
        int totalQuestions = 0;

        for (int i = 0; i < 4; i++)
        {
            totalCorrect += correctAnswers[i];
            totalQuestions += this.totalQuestions[i];
        }

        if (totalQuestions > 0)
        {
            float overallAccuracy = (float)totalCorrect / totalQuestions;

            if (overallAccuracy >= 0.8f && currentLevel < 8)
            {
                currentLevel++;
            }
            else if (overallAccuracy < 0.6f && currentLevel > 1)
            {
                currentLevel--;
            }
        }
    }
}