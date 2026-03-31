using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;

    public void OnButtonClick()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}