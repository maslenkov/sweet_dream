using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private const string _mainComics = "MainComics"; 

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(_mainComics);
    }
}
