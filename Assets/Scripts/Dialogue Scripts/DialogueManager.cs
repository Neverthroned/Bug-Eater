using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    [Header("Typewriter Settings")]
    public float typewriterSpeed = 0.04f;  // seconds per character

    private Queue<string> sentences = new();
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    public bool freeze = false;

    private PlayerWalk playerMovement;
    private PlayerCam playerCam;

    // Optional callback when dialogue finishes (used by special NPCs)
    private System.Action onDialogueFinished;

    //sets public for timer healthbar variable
    public TimerHealthBar timerHealthBar;

    void Start()
    {
        dialoguePanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();
    }

    // Starts dialogue and freezes player
    public void StartDialogue(Dialogue dialogue, System.Action onFinished = null)
    {
        freeze = true;
        onDialogueFinished = onFinished;

        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        //timerHealthbar_pause
        if (timerHealthBar != null)
            timerHealthBar.PauseTimer();


        dialoguePanel.SetActive(true);
        nameText.text = dialogue.characterName;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If still typing, skip to end of current sentence
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            isTyping = false;
            return;
        }

        // If no sentences, end dialogue
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    private string currentSentence = "";

    private IEnumerator TypeSentence(string sentence)
    {
        // Gets dialogue and types it out
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
        isTyping = false;
    }

    // Ends and unfreezes character
    void EndDialogue()
    {
        freeze = false;
        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");

        //resumes timer

        if (timerHealthBar != null)
            timerHealthBar.ResumeTimer();

        //If a special NPC requested an action after dialogue, run it
        onDialogueFinished?.Invoke();
        onDialogueFinished = null;

    }

    public bool IsOpen() => dialoguePanel.activeSelf;
}