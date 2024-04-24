using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationController : MonoBehaviour
{
    public void Play()
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

    }

    public void Battle(){
        SceneManager.LoadSceneAsync("BattleScene");
    }

    public void Treasure(){
        SceneManager.LoadSceneAsync("TreasureScene");
    }

    public void Restart(){
        SceneManager.LoadSceneAsync("ExplorationMenu");
    }

    public void GameOver(){
        SceneManager.LoadSceneAsync("GameOver");
    }
}
