using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    private const string MAIN_COMICS = "MainComics"; 
    private const string CREDITS = "CreditScene";

    public void OnExitClick()
    {
        Application.Quit();
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(MAIN_COMICS);
    }

    public void OnCreditsClick()
    {
        SceneManager.LoadScene(CREDITS);
    }
}
