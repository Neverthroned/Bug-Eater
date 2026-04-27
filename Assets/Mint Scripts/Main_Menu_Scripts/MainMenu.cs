using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TitleMenuUI;
    public GameObject aboutTitlePanel;
    public void StartGame()
    {
        SceneFadeManager.Instance.FadeToScene("Mint_Whitebox_V3");
    }

    public void ShowTitleAbout()
    {
        TitleMenuUI.SetActive(false);
        aboutTitlePanel.SetActive(true);
    }

    public void BackButton()
    {
        TitleMenuUI.SetActive(true);
        aboutTitlePanel.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("game closed!");

        Application.Quit();
    }
}
