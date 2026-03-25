using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject keypadPanel;

    public bool freeze = false;

    private PlayerWalk playerMovement;
    private PlayerCam playerCam;

    // Sets keypad to off on start and finds player scripts in order to freeze the player
    void Start()
    {
        keypadPanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();
    }

    // Starts keypad and freezes player
    public void StartKeypad()
    {
        freeze = true;
        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        keypadPanel.SetActive(true);
    }

    // Probably removable once actual logic is put in
    public void ExitKeypad()
    {
        EndKeypad();
    }

    void EndKeypad()
    {
        freeze = false;
        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        keypadPanel.SetActive(false);
    }
    public bool IsOpen() => keypadPanel.activeSelf;
}
