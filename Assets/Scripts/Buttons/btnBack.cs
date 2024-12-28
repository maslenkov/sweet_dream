using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnBack : MonoBehaviour
{
    private const string START_SCREEN = "StartScreen";
    public void OnBackClick()
    {
        SceneManager.LoadScene(START_SCREEN);
    }
}
