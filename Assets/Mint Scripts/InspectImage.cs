using UnityEngine;

public class InspectImage : MonoBehaviour, Interactable
{
    public GameObject inspectPanel;

    public string promptMessage = "Press E to Inspect";

    public void Interact()
    {
        inspectPanel.SetActive(true);
        Time.timeScale = 0f; // pause game
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public string GetPrompt()
    {
        return promptMessage;
    }

    void Update()
    {
        if (inspectPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInspect();
        }
    }

    void CloseInspect()
    {
        inspectPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
