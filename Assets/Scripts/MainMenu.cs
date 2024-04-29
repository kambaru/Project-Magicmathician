using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private string filePath;
    public GameObject playButton;
    public GameAI gameAI;
    public string userName;

    void Start()
    {
        filePath = Application.persistentDataPath + "/SaveData.json";
        Debug.Log("File path in start game ai: " + filePath);
        checkFile();
    }

    public void checkFile()
    {
        Debug.Log("File path in load game ai: " + filePath);
        userName = gameAI.GetUserName();
        if (userName == ""){
            playButton.SetActive(false);
        }
    }
}
