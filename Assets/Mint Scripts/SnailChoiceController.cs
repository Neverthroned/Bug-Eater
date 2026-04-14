using UnityEngine;
using UnityEngine.SceneManagement;

public class SnailChoiceController : MonoBehaviour
{
    [Header("Dialogues")]
    public Dialogue introDialogue;
    public Dialogue yesDialogue;
    public Dialogue noDialogue;

    [Header("References")]
    public ChoicePanelController choicePanel;

    private DialogueManager dialogueManager;

    // 0 = first meeting
    // 1 = refused before
    // 2 = eaten
    private int snailState = 0;

    void Start()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }

    // Called by DialogueTrigger
    public void StartSnailConversation()
    {
        if (snailState == 2) return;

        if (snailState == 0)
        {
            dialogueManager.StartDialogue(introDialogue, choicePanel.ShowChoice);
        }
        else if (snailState == 1)
        {
            choicePanel.ShowChoice();
        }
    }

    public void ChooseYes()
    {
        choicePanel.CloseChoice();
        snailState = 2;

        // After dialogue eat snail
        dialogueManager.StartDialogue(yesDialogue, EatSnail);
    }

    public void ChooseNo()
    {
        GameManager.Instance.saidNoToSnail = true;
        choicePanel.CloseChoice();
        snailState = 1;

        // After dialogue return to previous scene
        dialogueManager.StartDialogue(noDialogue, ReturnPlayerToMainScene);
    }

    void EatSnail()
    {
        Destroy(gameObject);

        SceneManager.LoadScene("EndingScene");
    }

    void ReturnPlayerToMainScene()
    {
        GameManager.Instance.ReturnToPreviousScene();
    }
}