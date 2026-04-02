using UnityEngine;
using UnityEngine.UI; 

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

    void Start()
    {
        currentTime = maxTime;
        
    }

    public void ResetTimer()
    {
    currentTime = maxTime;
    timerSpeed = 1f;
    isFlashing = false;

    UpdateHealthBarUI();
    }

    void Update()
    {
        if (isPaused) return;

        if (currentTime > 0)
        {
            
            currentTime -= Time.deltaTime * timerSpeed; 
            
            
            currentTime = Mathf.Clamp(currentTime, 0f, maxTime);

            
            UpdateHealthBarUI();
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

    public void PauseTimer()
    {
        isPaused = true;

        if (hungerUIPanel != null)
            hungerUIPanel.SetActive(false);
    }

    public void ResumeTimer()
    {
        isPaused = false;

        if (hungerUIPanel != null)
            hungerUIPanel.SetActive(true);
    }

    public bool IsPaused()
    {
        return isPaused;
    }


}
