using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomDoorGenerator : MonoBehaviour
{
    public Button Door1;
    public Button Door2;
    public Button Door3;
    public Button Door4;

    void Start()
    {
        AssignDoorsRandomly();
    }

    // Doors are randomly assigned by an integer between 0-2 randomly generated.
    public void AssignDoorsRandomly()
    {
        int randInd1 = Random.Range(0, 3);
        int randInd2 = Random.Range(0, 3);
        OpenDoors(randInd1, randInd2);
    }

    public void OpenDoors(int randInd1, int randInd2){

        switch (randInd1)
        {
            case 0:
                Door1.gameObject.SetActive(true);
                break;
            case 1:
                Door1.gameObject.SetActive(true);
                break;
            case 2:
                Door3.gameObject.SetActive(true);
                break;
        }

        switch (randInd2){
            case 0:
                Door2.gameObject.SetActive(true);
                break;
            case 1: 
                Door22.gameObject.SetActive(true);
                break;
            case 2: 
                Door4.gameObject.SetActive(true);
                break;
        }
    }

}