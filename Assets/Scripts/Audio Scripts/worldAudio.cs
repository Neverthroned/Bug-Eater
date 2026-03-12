using UnityEngine;

public class worldAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip audioClip1;
    public AudioClip audioClip2;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float volumeClip1 = 1f;
    [Range(0f, 1f)] public float volumeClip2 = 1f;

    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Awake()
    {
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        audioSource1.clip   = audioClip1;
        audioSource1.volume = volumeClip1;
        audioSource1.loop   = true;
        audioSource1.Play();

        audioSource2.clip   = audioClip2;
        audioSource2.volume = volumeClip2;
        audioSource2.loop   = true;
        audioSource2.Play();
    }

    public void PauseAll()
    {
        audioSource1?.Pause();
        audioSource2?.Pause();
    }

    public void ResumeAll()
    {
        audioSource1?.UnPause();
        audioSource2?.UnPause();
    }

    public void StopAll()
    {
        audioSource1?.Stop();
        audioSource2?.Stop();
    }
}
