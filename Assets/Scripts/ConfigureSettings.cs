using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ConfigureSettings : MonoBehaviour
{
    public GameObject settingsUI;
    public AudioMixer masterMixer;

    public void CloseSettings(){
        settingsUI.SetActive(false);
    }

    public void SetMaster(float soundLevel){
        masterMixer.SetFloat("MasterVol", soundLevel);
    }

    public void SetMusic(float soundLevel){
        masterMixer.SetFloat("MusicVol", soundLevel);
    }

    public void SetSfx(float soundLevel){
        masterMixer.SetFloat("SfxVol", soundLevel);
    }
}
