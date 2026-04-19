using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGameScene1()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void LoadGameScene2()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadMatchHistory()
    {
        SceneManager.LoadScene("MatchHistory");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}