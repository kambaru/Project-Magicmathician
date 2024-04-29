using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    //setting the slider's value as health so that it can decrease and increase
    public void SetHealthBar(int health)
    {
        slider.value = health;
    }

    
}