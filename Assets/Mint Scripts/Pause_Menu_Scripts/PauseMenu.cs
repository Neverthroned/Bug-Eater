using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject controlsPanel;
    public GameObject aboutPanel;

    bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        controlsPanel.SetActive(false);
        aboutPanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        controlsPanel.SetActive(false);
        aboutPanel.SetActive(false);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isPaused = true;
    }

    public void ShowControls()
    {
        pauseMenuUI.SetActive(false);
        controlsPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }

    public void ShowAbout()
    {
        pauseMenuUI.SetActive(false);
        aboutPanel.SetActive(true);
        controlsPanel.SetActive(false);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneFadeManager.Instance.FadeToScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToTitle()
    {
        Time.timeScale = 1f;
        SceneFadeManager.Instance.FadeToScene("MainMenu");
    }
}