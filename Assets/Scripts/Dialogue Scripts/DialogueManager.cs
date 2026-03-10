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

    void Start()
    {
        dialoguePanel.SetActive(false);
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        freeze = true;
        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        dialoguePanel.SetActive(true);
        nameText.text = dialogue.characterName;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();
    }

    // Call this from a UI "Next" button or wire it to the Interact action
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
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in sentence)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typewriterSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        freeze = false;
        playerMovement?.SetFreeze(false);
        playerCam?.SetFreeze(false);

        dialoguePanel.SetActive(false);
        Debug.Log("Dialogue ended.");
    }

    public bool IsOpen() => dialoguePanel.activeSelf;
}