using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

//customizes the welcome message that appears on the home screen
public class HomeInfo : MonoBehaviour
{
    public GameAI gameAI;
    public TMP_Text profileText;


    void Start()
    {
        gameAI.LoadProgress();
        string username = gameAI.GetUserName();
        Debug.Log("HomeInfo username is " + username);
        profileText.text = "Welcome " + username;
    }
}
