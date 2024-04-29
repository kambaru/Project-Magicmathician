using UnityEngine;
using System.Collections.Generic;
using System.IO;

/* Handles all the file managament (such as retrieving and storing player data) and 
 * and recording the player's performance. 
 */
public class GameAI : MonoBehaviour
{
    private string filePath;
    public string userName;
    public int[] correctAnswers = new int[4]; // [addition, subtraction, multiplication, division]
    public int[] totalQuestions = new int[4];
    public int[] currentLevel = new int[4]; // level will determine the difficulty of the question.
    public int[] buffs = new int[3]; // stores the buffs [attack, health, mana]
    public int coins = 0;

    [System.Serializable]
    public class PlayerRecord
    {
        [SerializeField] public string UserName;
        [SerializeField] public int[] CorrectAnswers; // [addition, subtraction, multiplication, division]
        [SerializeField] public int[] TotalQuestions;
        [SerializeField] public int[] CurrentLevel;
        [SerializeField] public int[] Buffs;
        [SerializeField] public int Coins;
        
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("File path in start game ai: " + filePath);
        LoadProgress();
    }

    public void NewGame()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        Debug.Log("File path in save game ai: " + filePath);
        var playerRecord = new PlayerRecord
        {
            UserName = userName,
            CorrectAnswers = correctAnswers,
            TotalQuestions = totalQuestions,
            CurrentLevel = currentLevel,
            Buffs = buffs,
            Coins = coins            
        };

        string jsonString = JsonUtility.ToJson(playerRecord);
        Debug.Log("SaveProgress The json string is " + jsonString);

        File.WriteAllText(filePath, jsonString);
    }

    public void SaveProgress(string username){
        userName = username;
        correctAnswers = new int[4]; // [addition, subtraction, multiplication, division]
        totalQuestions = new int[4];
        currentLevel = new int[4]; // level will determine the difficulty of the question.
        buffs = new int[3]; // stores the buffs [attack, health, mana]
        coins = 0;
        Debug.Log("File path in NEW save game ai: " + filePath);
        SaveProgress();
    }

    public void LoadProgress()
    {
        Debug.Log("File path in load game ai: " + filePath);
        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            if (data != "{}")
            {
                Debug.Log("PlayerRecord data is: " + data);
                PlayerRecord playerRecord = JsonUtility.FromJson<PlayerRecord>(data);
                userName = playerRecord.UserName;
                correctAnswers = playerRecord.CorrectAnswers;
                totalQuestions = playerRecord.TotalQuestions;
                currentLevel = playerRecord.CurrentLevel;
                buffs = playerRecord.Buffs;
                coins = playerRecord.Coins;
            }
            else
            {
                SaveProgress();
            }
        }
        else
        {
            SaveProgress();
        }

    }

    public void GainCoins()
    {
        int randCoins = Random.Range(10, 26);
        coins += randCoins;
        SaveProgress();
    }

    public int GetCoins()
    {
        return coins;
    }

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
        Debug.Log("Start SaveProgress: " + logMessage);
        SaveProgress();

    }

    public void UpdateCorrectAnswers(int topic)
    {
        correctAnswers[topic]++;
    }

    public void UpdateTotalQuestions(int topic)
    {
        totalQuestions[topic]++;
    }

    public void UpdateBuff(int index, int increaseValue, int cost)
    {
        coins = coins - cost;
        buffs[index] = buffs[index] + increaseValue;
        SaveProgress();
    }

    public int[] GetBuffs(){
        return buffs;
    }

    private float[] getAccuracy()
    {
        float[] accuracy = new float[4];

        for (int i = 0; i < accuracy.Length; i++)
        {
            if (totalQuestions[i] == 0)
            {
                accuracy[i] = 0f;
            }
            else
            {
                accuracy[i] = (float)correctAnswers[i] / totalQuestions[i];
            }
        }
        return accuracy;

    }

    /* 
    */
    public int GetWeakestTopic()
    {
        float[] accuracy = getAccuracy();
        
        float minAcc = accuracy[0];
        int minTopic;
        List<int> topics = new List<int>();
        topics.Add(0);
    
        for (int i = 1; i < accuracy.Length; i++)
        {
            if (Mathf.Abs(minAcc - accuracy[i]) <= 0.05)
            {
                if (minAcc > accuracy[i])
                {
                    minAcc = accuracy[i];
                }
                topics.Add(i);
            }
            else if (minAcc - accuracy[i] > 0.05)
            {
                minAcc = accuracy[i];
                topics = new List<int>();
                topics.Add(i);
            }
        }

        int randomIndex = Random.Range(0, topics.Count);
        Debug.Log("The random number is: " + randomIndex + " the number of topics is: " + topics.Count);
        minTopic = topics[randomIndex];

        return minTopic;
    }

    public int etTopicLevel(int index)
    {
        return currentLevel[index];
    }

    public string getOverallLevel(){
        int sum = 0;
        for (int i = 0; i < 4; i ++)
        {
            sum += getTopicLevel(i);
        }
        int difficulty = (Mathf.RoundToInt((float)sum / 4));
        string[] proficiency = {"Beginner", "Amateur", "Expert", "Master"};
        return proficiency[difficulty];
    }

    public string[] getStatistics(){
        UpdateDifficulty();
        float[] accuracy = getAccuracy();
        string[] statistics = new string[4];
        for (int i = 0; i < 4; i ++)
        {
            int acc = Mathf.RoundToInt(accuracy[i] * 100);
            statistics[i] = acc.ToString() + "%";
        }
        return statistics;
    }

    public string GetUserName()
    {
        Debug.Log("Get username is: " + userName);
        return userName;
    }

    public void UpdateDifficulty()
    {
        float[] accuracy = getAccuracy();
        for (int i = 0; i < 4; i ++)
        {
            // if user has answered 0 questions, they are at beginner = 0
            if (totalQuestions[i] == 0)
            {
                currentLevel[i] = 0;
            }
            // if user has answered 5 questions, and their answer rate is 80%+, they can go to amateur = 1
            else if (totalQuestions[i] >= 5 && totalQuestions[i] < 10 && accuracy[i] > 0.8)
            {
                currentLevel[i] = 1;
            }
            // if user has answered 10 questions and their answer rate is 70%+, they can go to expert = 2
            else if (totalQuestions[i] >= 10 && totalQuestions[i] < 20 && accuracy[i] > 0.7)
            {
                currentLevel[i] = 2;
            }
            // if user has answered 20 questions and their answer rate is 60%+, they can go to master = 3
            else if (totalQuestions[i] >= 20 && accuracy[i] > 0.6)
            {
                currentLevel[i] = 3;
            }
            // if user's answer rate has fallen to <50%, they will go down by 1 difficulty level.
            else if (totalQuestions[i] >= 5 && accuracy[i] < 0.5)
            {
                currentLevel[i] = currentLevel[i] - 1;
            }

        }
        SaveProgress();       
    }

}