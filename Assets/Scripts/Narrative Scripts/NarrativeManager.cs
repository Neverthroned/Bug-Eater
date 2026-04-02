using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NarrativeManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject narrativePanel;
    public Image narrativeDisplay;

    public bool freeze = false;

    private PlayerWalk playerMovement;
    private PlayerCam playerCam;

    //sets public for timer healthbar variable
    public TimerHealthBar timerHealthBar;

    // Sets keypad to off on start and finds player scripts in order to freeze the player
    void Start()
    {
        narrativePanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();
    }

    // Starts keypad and freezes player
    public void StartNarrative(Sprite image)
    {
        if (image != null)
            narrativeDisplay.sprite = image;

        freeze = true;
        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);
        narrativePanel.SetActive(true);

        //timerHealthbar_pause
        if (timerHealthBar != null)
            timerHealthBar.PauseTimer();
    }

    // Probably removable once actual logic is put in
    public void ExitNarrative()
    {
        EndNarrative();
    }

    void EndNarrative()
    {
        freeze = false;
        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        narrativePanel.SetActive(false);

        //resumes timer

        if (timerHealthBar != null)
            timerHealthBar.ResumeTimer();
    }
    public bool IsOpen() => narrativePanel.activeSelf;
}
