using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameControl;

    public int score;

    //can put the player stats here?
    
    private void Awake(){

        if (gameControl == null) {
            gameControl = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (gameControl != this){
            Destroy(gameObject);
        }
    }

}
