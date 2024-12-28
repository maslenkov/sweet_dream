using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnMute : MonoBehaviour
{
    public void OnMuteClick()
    {
        AudioListener.pause = !AudioListener.pause;
    }
}
