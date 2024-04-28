using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MessageController : MonoBehaviour
{

    public TMP_Text messageText;

    public void showMessage(string msg)
    {
        messageText.text = msg;
    }
}
