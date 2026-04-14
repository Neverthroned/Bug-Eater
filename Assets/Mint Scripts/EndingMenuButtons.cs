using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenuButtons : MonoBehaviour
{
    public string mainMenuSceneName = "MainMenu";

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}