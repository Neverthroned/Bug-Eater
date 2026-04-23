using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;

    private AudioSource audioSource;

    [Header("Sound Effects")]
    public AudioClip eatBugSFX;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEatSound()
    {
        audioSource.PlayOneShot(eatBugSFX);
    }
}