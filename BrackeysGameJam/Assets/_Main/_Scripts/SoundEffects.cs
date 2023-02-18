using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour
{
    public static SoundEffects Instance { get; private set; }

    AudioSource audioSource;

    public float GetVolume() => audioSource.volume;
    public float SetVolume() => audioSource.volume;

    public void PlayClip(AudioClip clip) => audioSource.PlayOneShot(clip);
    public void PlayClip(AudioClip clip, float volumeScale) => audioSource.PlayOneShot(clip, volumeScale);
    public void StopClip() => audioSource.Stop();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        audioSource = GetComponent<AudioSource>();
    }
}