using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// script for allowing the player to earn coins during their runs
public class TreasureEvent : MonoBehaviour
{
    public GameAI gameAI;

    public void GainCoins(){
        gameAI.GainCoins();
        continueExploring();
    }

    public void continueExploring(){
        SceneManager.LoadSceneAsync("ExplorationMenu");
    }

}
