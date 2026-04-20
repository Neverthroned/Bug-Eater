using UnityEngine;

public class ChoicePanelController : MonoBehaviour
{
    public GameObject choicePanel;

    PlayerWalk playerMovement;
    PlayerCam playerCam;


    //player interaction to prevent e to interact
    PlayerInteraction playerInteraction;

    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();

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

    public void CloseChoiceResumeGameplay()
    {
        choicePanel.SetActive(false);

        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Gameplay resumes  interaction ends
        playerInteraction?.EndInteraction();
    }

    public void CloseChoiceContinueInteraction()
    {
        choicePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }
}