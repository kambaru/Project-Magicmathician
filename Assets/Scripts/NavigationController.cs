using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public void Explore()
    {
        SceneManager.LoadSceneAsync("ExplorationMenu");
    }

    public void Settings(){

    }

    public void NewGame(){

    }

    public void LoadGame(){

    }

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
