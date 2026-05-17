using UnityEngine;

public class CanvasButtons : MonoBehaviour
{
    public void ReplayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
