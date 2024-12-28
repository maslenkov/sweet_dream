using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeController : MonoBehaviour
{
    private void Update()
    {
        // if escape set user pref pause to true
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PlayerPrefs.GetInt("Pause", 0) == 0)
            {
                PlayerPrefs.SetInt("Pause", 1);
            } else {
                PlayerPrefs.SetInt("Pause", 0);
            }
        }
    }
}
