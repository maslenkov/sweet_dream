using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    public AudioSource musicSource;

    public AudioClip _musicClip1;
    public AudioClip _musicClip2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("Mute", 0) == 0)
        {
            musicSource.mute = false;
        }
        else
        {
            musicSource.mute = true;
        }
    }
}
