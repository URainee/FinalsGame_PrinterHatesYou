using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("DialogueScreen");
    }

    public void Options()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
