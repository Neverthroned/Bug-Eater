using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    [Header("Buttons")]
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;

    private void Start()
    {
        button1.onClick.AddListener(PlayButtonSound);
        button2.onClick.AddListener(PlayButtonSound);
        button3.onClick.AddListener(PlayButtonSound);
        button4.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }

    private void OnDestroy()
    {
        button1.onClick.RemoveListener(PlayButtonSound);
        button2.onClick.RemoveListener(PlayButtonSound);
        button3.onClick.RemoveListener(PlayButtonSound);
        button4.onClick.RemoveListener(PlayButtonSound);
    }
}
