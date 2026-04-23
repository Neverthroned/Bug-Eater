using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance;

    private AudioSource sfxSource;
    private AudioSource footstepSource;

    [Header("Sound Effects")]
    public AudioClip eatBugSFX;

    [Header("Footsteps")]
    public AudioClip footstepLoop;

    void Awake()
    {
        Instance = this;

        AudioSource[] sources = GetComponents<AudioSource>();
        sfxSource = sources[0];
        footstepSource = sources[1];

        footstepSource.clip = footstepLoop;
    }

    public void PlayEatSound()
    {
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(eatBugSFX);
    }

    // FOOTSTEP CONTROL
    public void StartFootsteps()
    {
        if (!footstepSource.isPlaying)
            footstepSource.Play();
    }

    public void StopFootsteps()
    {
        if (footstepSource.isPlaying)
            footstepSource.Stop();
    }
}