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

    //sets public for timer healthbar variable
    public TimerHealthBar timerHealthBar;

    //helps with keypad button glow
    KeypadButtonGlow[] allButtons;

    [Header("Keypad Hint Images")]
    public GameObject[] hintImages; // drag 4 UI images here

    private bool[] unlockedHints;

    // Sets keypad to off on start and finds player scripts in order to freeze the player
    void Start()
    {
        keypadPanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();

        //determines which hints have been unlocked
        unlockedHints = new bool[hintImages.Length];

        // hide all hints at game start
        foreach (var img in hintImages)
            img.SetActive(false);
    }

    void UpdateHintUI()
    {
        for (int i = 0; i < hintImages.Length; i++)
        {
            hintImages[i].SetActive(unlockedHints[i]);
        }
    }

    // Starts keypad and freezes player
    public void StartKeypad()
    {
        freeze = true;
        currentInput = ""; // resets password input

        UpdateHintUI(); 

        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        keypadPanel.SetActive(true);
        allButtons = keypadPanel.GetComponentsInChildren<KeypadButtonGlow>(true);

        //Show the mouse for the player
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //timerHealthbar_pause
        if (timerHealthBar != null)
            timerHealthBar.PauseTimer();

        //resets the button
        ResetButtons();
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

    //unlocks the hints
    public void UnlockHint(int hintIndex)
    {
        if (hintIndex < 0 || hintIndex >= unlockedHints.Length)
            return;

        unlockedHints[hintIndex] = true;
        Debug.Log("Unlocked keypad hint #" + hintIndex);
    }

    private void CheckCode()
    {
        if (currentInput == correctCode)
        {
            Debug.Log("Correct password!");

            GameManager.Instance.returnScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                GameManager.Instance.returnPosition = player.transform.position;
            }
            else
            {
                Debug.LogError("Player not found when saving position!");
            }

            GameManager.Instance.LoadSnailScene();
        }
        else
        {
            Debug.Log("Wrong password, try again.");
            WrongPassword();       //  flash red
            currentInput = "";
        }
    }

    //handles if the password is wrong
    void WrongPassword()
    {
        foreach (var btn in allButtons)
            btn.ErrorGlow();
    }

    //if password is right.
    void ResetButtons()
    {
        foreach (var btn in allButtons)
            btn.ResetGlow();
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

    public void EndKeypad()
    {
        freeze = false;
        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        keypadPanel.SetActive(false);

        // Hide and lock cursor again
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //resumes timer

        if (timerHealthBar != null)
            timerHealthBar.ResumeTimer();

        FindFirstObjectByType<PlayerInteraction>().EndInteraction();
    }
    public bool IsOpen() => keypadPanel.activeSelf;
}
