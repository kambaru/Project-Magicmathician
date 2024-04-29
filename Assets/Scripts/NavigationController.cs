using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/* Contains the navigation system which controls where the player
 * will go upon selecting an option.
 */

public class NavigationController : MonoBehaviour
{
    public GameObject settingsUI;

    public void Explore()
    {
        SceneManager.LoadSceneAsync("ExplorationMenu");
    }

    // shows the settings which were previously disabled
    public void Settings(){
        settingsUI.SetActive(true);
    }

    public void NewGame(){
        SceneManager.LoadSceneAsync("NewPlayerScene");

    }

    // quits the application
    public void Exit(){
        Application.Quit();
    }

    public void BuffScene(){
        SceneManager.LoadSceneAsync("UpgradeBuffMenu");
    }


    public void Battle(){
        SceneManager.LoadSceneAsync("BattleScene");
    }

    public void Treasure(){
        SceneManager.LoadSceneAsync("TreasureScene");
    }

    public void ResultsShow(){
        SceneManager.LoadSceneAsync("ResultsScene");
    }

    public void GoHome(){
        SceneManager.LoadSceneAsync("HomeScene");
    }
}
