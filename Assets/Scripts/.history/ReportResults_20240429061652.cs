using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

//
public class ReportResults : MonoBehaviour
{
    public GameAI reportObject;
    public TMP_Text titleText;
    public TMP_Text addition;
    public TMP_Text subtraction;
    public TMP_Text multiplication;
    public TMP_Text division;
    public TMP_Text overallLevel;
    public TMP_Text currentCoins;


    void Start()
    {
        Debug.Log("File path in report results: ");
        reportObject.LoadProgress();
        string level = reportObject.GetOverallLevel();
        string[] stats = reportObject.GetStatistics();
        string userName = reportObject.GetUserName();
        titleText.text = userName + "'s Overall Performance";
        addition.text = stats[0];
        subtraction.text = stats[1];
        multiplication.text = stats[2];
        division.text = stats[3];
        overallLevel.text = level;
        currentCoins.text = reportObject.coins.ToString();
    }


}
