using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomDoorGenerator : MonoBehaviour
{
    public Button Door1;
    public Button Door2;
    public Button Door11;
    public Button Door22;

    void Start()
    {
        // Randomly assign Battle or Treasure to each door
        AssignDoorsRandomly();
    }

    void AssignDoorsRandomly()
    {
        //do random int between 0 and 1
        //each door gets a label
        //set all doors to non-visible
        //only show the correct doors as visible
        int randInd1 = Random.Range(0, 2);
        int randInd2 = Random.Range(0, 2);

        OpenDoors(randInd1, randInd2);
    }

    void OpenDoors(int randInd1, int randInd2){

        switch (randInd1)
        {
            case 0:
                Door1.gameObject.SetActive(true);
                break;

            case 1:
                Door11.gameObject.SetActive(true);
                break;
        }

        switch (randInd2){
            case 0:
                Door2.gameObject.SetActive(true);
                break;

            case 1: 
                Door22.gameObject.SetActive(true);
                break;
        }
    }

}