using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

//Class for the shop which allows you to purchase buffs.
//These buffs get permanently added to the player's stats.
public class Shop : MonoBehaviour
{
    public GameAI gameAI;
    public TMP_Text messageText;
    public TMP_Text coinText;

    void Update(){
        setCoins();
    }

    public void setCoins()
    {
        coinText.text = gameAI.GetCoins().ToString();
    }

    public void setMessage(string message)
    {
        messageText.text = message;
    }

    public void SmallAttackBuff()
    {
        int index = 0;
        int buff = 2;
        int cost = 50;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the small attack buff!\nYour attack is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }

    public void SmallHealthBuff()
    {
        int index = 1;
        int buff = 5;
        int cost = 100;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the small health buff!\nYour health is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }

    public void SmallManaBuff()
    {
        int index = 2;
        int buff = 5;
        int cost = 100;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the small mana buff!\nYour mana is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }

    public void BigAttackBuff()
    {
        int index = 0;
        int buff = 5;
        int cost = 100;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the big attack buff!\nYour attack is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }

    public void BigHealthBuff()
    {
        int index = 1;
        int buff = 10;
        int cost = 200;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the big health buff!\nYour health is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }

    public void BigManaBuff()
    {
        int index = 2;
        int buff = 10;
        int cost = 200;
        int coins = gameAI.GetCoins();
        string message;
        if (coins < cost)
        {
            message = "Unfortunately, you don't have enough coins to purchase";
        }
        else
        {
            gameAI.UpdateBuff(index, buff, cost);
            int newVal = gameAI.GetBuffs()[index];
            message = "You have purchase the big mana buff!\nYour mana is now " + newVal;
        }
        setMessage(message);
        setCoins();
    }



}
