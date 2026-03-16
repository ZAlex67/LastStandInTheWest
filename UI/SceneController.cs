using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(sceneIndex);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}