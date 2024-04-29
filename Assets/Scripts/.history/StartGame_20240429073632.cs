using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

//script for the starting scene for new game
public class StartGame : MonoBehaviour
{
    public TMP_InputField nameText;
    public TMP_Text questionText;
    public TMP_Text messageText;
    public GameObject inputObj;
    public GameObject confirmationObj;
    public GameObject welcomeObj;

    public GameAI gameAI;

    public string userName;

    public void getName()
    {
        userName = nameText.text;
        inputObj.SetActive(false);
        string message = "Are you sure your name is " + userName + "?";
        questionText.text = message;
        confirmationObj.SetActive(true);
    }

    public void yesName()
    {
        confirmationObj.SetActive(false);
        createNewGame();
        string message = "Welcome " + userName + " to the land of Terra.\n\nYou are the last surviving Magicmathician and it's up  to you to restore the fertile soil of the barren world.\n\nGo forth and bring blessings throughout the land and destroy the evil lurking deeply in the depths.";
        messageText.text = message;
        welcomeObj.SetActive(true);
    }

    public void noName()
    {
        confirmationObj.SetActive(false);
        inputObj.SetActive(true);
    }

    public void createNewGame()
    {
        gameAI.SaveProgress(userName);
    }

}
