using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public static UIAudioManager Instance;

    public AudioSource audioSource;
    public AudioClip buttonClick;

    void Awake()
    {
        // Singleton so any button can access it
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayClick()
    {
        if (buttonClick != null)
            audioSource.PlayOneShot(buttonClick);
    }
}
