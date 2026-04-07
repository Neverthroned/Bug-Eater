using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerHealthBar : MonoBehaviour
{
    [Header("UI")]
    public GameObject hungerUIPanel;

    public Image healthBarFill;

    public float maxTime = 60f; 
    private float currentTime;
    private float timerSpeed = 1f;

    public float flashSpeed = 5f;
    private bool isFlashing = false;

    [Header("Death UI")]
    public GameObject deathPanel;

    [Header("Optional UI To Hide On Death")]
    public GameObject dialoguePanel;
    public GameObject inspectPanel;
    public GameObject keypadPanel;
    public GameObject interactPromptUI;

    //pausing bool so hunger bar pauses during dialogue.
    private bool isPaused = false;

    //death bool...OF DEATH!
    private bool isDead = false;

    private PlayerWalk playerMovement;
    private PlayerCam playerCam;

    void Start()
    {
        currentTime = maxTime;


        //needed for death UI scene script.
        playerMovement = FindFirstObjectByType<PlayerWalk>();
        playerCam = FindFirstObjectByType<PlayerCam>();

        if (deathPanel != null)
            deathPanel.SetActive(false);

        UpdateHealthBarUI();
    }

    public void ResetTimer()
    {

        currentTime = maxTime;
        timerSpeed = 1f;
        isFlashing = false;

        isPaused = false;
        isDead = false;

        if (deathPanel != null)
           deathPanel.SetActive(false);

        if (hungerUIPanel != null)
           hungerUIPanel.SetActive(true);

         UpdateHealthBarUI();

    }

    void Update()
    {
        if (isPaused || isDead) return;

        if (currentTime > 0)
        {
            
            currentTime -= Time.deltaTime * timerSpeed; 
            
            
            currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

            
            UpdateHealthBarUI();

            //time to die!
            if (currentTime <= 0f && !isDead)
            {
                Die();
            }
        }
        else
        {
            
            Debug.Log("Timer Finished!");
        }
    }

    void UpdateHealthBarUI()
    {
        
        float fillAmount = currentTime / maxTime;

        
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = fillAmount;
        }

        if (fillAmount > 0.6f)
        {
            healthBarFill.color = Color.green;
            isFlashing = false;
            
            
        }
        else if (fillAmount > 0.3f)
        {
            healthBarFill.color = Color.yellow;
            timerSpeed = 0.5f;
            isFlashing = false;
        }
        else
        {
            healthBarFill.color = Color.red;
            timerSpeed = 0.25f;
            isFlashing = true;

            float alpha = Mathf.PingPong(Time.time * flashSpeed, 1f);
            healthBarFill.color = new Color(1f, 0f, 0f, alpha);
            
        }
    }

    //handles death. Ooooh I can't decide, whether you should live or die.
    void Die()
    {
        isDead = true;
        isPaused = true;

        Debug.Log("Player died of starvation.");

        // Freeze player
        playerMovement?.SetFreeze(true);
        playerCam?.SetFreeze(true);

        // Hide normal UI
        if (hungerUIPanel != null)
            hungerUIPanel.SetActive(false);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (inspectPanel != null)
            inspectPanel.SetActive(false);

        if (keypadPanel != null)
            keypadPanel.SetActive(false);

        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        // Show death screen
        if (deathPanel != null)
            deathPanel.SetActive(true);

        // Bring back mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void PauseTimer()
    {
        if (isDead) return;

        isPaused = true;

        if (hungerUIPanel != null)
            hungerUIPanel.SetActive(false);
    }

    public void ResumeTimer()
    {
        if (isDead) return;

        isPaused = false;

        if (hungerUIPanel != null)
            hungerUIPanel.SetActive(true);
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartSceneFromButton()
    {
        RestartScene();
    }

    public void QuitToRestartAnyway()
    {
        RestartScene();
    }
}
