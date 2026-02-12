using UnityEngine;
using UnityEngine.UI; 

public class TimerHealthBar : MonoBehaviour
{
    
    public Image healthBarFill;

     

    public float maxTime = 60f; 
    private float currentTime;
    private float timerSpeed = 1f;

    void Start()
    {
        currentTime = maxTime;
        
    }

    void Update()
    {
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
            
        }
        else if (fillAmount > 0.3f)
        {
            healthBarFill.color = Color.yellow;
            timerSpeed = 0.5f;
        }
        else
        {
            healthBarFill.color = Color.red;
            timerSpeed = 0.25f;
            
        }
    }
}
