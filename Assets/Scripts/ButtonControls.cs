using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControls : MonoBehaviour
{

    public GameObject settings;

    public void SettingsMenu()
    {
        if (!settings.activeInHierarchy)
        {
            settings.SetActive(true);
        } else
        {
            settings.SetActive(false);
        }

    }
    public void PlayDustMusic()
    {
        FindObjectOfType<AudioManagerScript>().Play("DustInWind");
    }
    public void StopDustMusic()
    {
        FindObjectOfType<AudioManagerScript>().Stop("DustInWind");
    }
    public void BackToSceneZero()
    {
        //settings.SetActive(false);
    }
}
