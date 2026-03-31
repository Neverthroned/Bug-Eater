using UnityEngine;
using UnityEngine.SceneManagement;

public class KeypadManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject keypadPanel;

    // handles keypad puzzle logic
    [Header("Password Settings")]
    [SerializeField] private string correctCode = "5429";
    [SerializeField] private int maxCodeLength = 4;

    private string currentInput = "";

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
        currentInput = ""; // resets password input

        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        keypadPanel.SetActive(true);

        //Show the mouse for the player
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    //called by each button, what really makes the keypad work!
    public void PressKey(string value)
    {
        if (currentInput.Length >= maxCodeLength)
            return;

        currentInput += value;
        Debug.Log("Current input: " + currentInput);

        if (currentInput.Length == maxCodeLength)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct password!");
            EndKeypad();
        }
        else
        {
            Debug.Log("Wrong password, try again.");
            currentInput = "";
        }
    }

    // Optional clear button
    public void ClearInput()
    {
        currentInput = "";
        Debug.Log("Input cleared.");
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

        // Hide and lock cursor again
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public bool IsOpen() => keypadPanel.activeSelf;
}
