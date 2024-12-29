using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnMute : MonoBehaviour
{
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            OnMuteClick();
        }
    }

    public void OnMuteClick()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            PlayerPrefs.SetInt("Mute", 1);
        } else {
            PlayerPrefs.SetInt("Mute", 0);
        }
    }
}
