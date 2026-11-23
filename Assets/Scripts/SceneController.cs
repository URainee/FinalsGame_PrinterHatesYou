using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SkipToGame()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void GoodEnd()
    {
        SceneManager.LoadScene("GoodEndingScreen");
    }

    public void BadEnd()
    {
        SceneManager.LoadScene("BadEndingScreen");
    }

    public void BrokeEnd()
    {
        SceneManager.LoadScene("BrokeEndingScreen");
    }
}
