using UnityEngine;

public class ChoicePanelController : MonoBehaviour
{
    public GameObject choicePanel;

    PlayerWalk playerMovement;
    PlayerCam playerCam;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();

        choicePanel.SetActive(false);
    }

    public void ShowChoice()
    {
        choicePanel.SetActive(true);

        // freeze player again
        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void CloseChoice()
    {
        choicePanel.SetActive(false);

        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}