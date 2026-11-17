using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public void Return()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
