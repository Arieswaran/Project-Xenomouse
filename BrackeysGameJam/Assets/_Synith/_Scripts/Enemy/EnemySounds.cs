using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySounds : MonoBehaviour
{
    public AudioClip
    walkSound,
    bushSound,
    detectSound,
    growlSound;

    AudioSource catAudio;
    Enemy enemy;


    void PlaySound(AudioClip clip) => catAudio.PlayOneShot(clip);
    void PlaySound(AudioClip clip, float volumeScale) => catAudio.PlayOneShot(clip, volumeScale);

    void Awake()
    {
        catAudio = GetComponent<AudioSource>();
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        enemy.OnDetectMouse += Enemy_OnDetectMouse;
        enemy.OnInBushChanged += Enemy_OnInBushChanged;
        enemy.OnTakeStep += Enemy_OnTakeStep;
        enemy.OnStopChase += Enemy_OnStopChase;

        SetVolume();
    }

    private void Enemy_OnStopChase()
    {
        PlaySound(growlSound);
    }

    void SetVolume()
    {
        float gameVolume = SoundEffects.Instance.GetVolume();
        catAudio.volume = gameVolume;
    }

    private void Enemy_OnTakeStep()
    {
        PlaySound(walkSound);
    }

    private void Enemy_OnInBushChanged(bool isInBush)
    {
        if (isInBush)
            PlaySound(bushSound, 0.5f);
    }

    private void Enemy_OnDetectMouse()
    {
        PlaySound(detectSound);
    }

    void OnDestroy()
    {
        enemy.OnDetectMouse -= Enemy_OnDetectMouse;
        enemy.OnInBushChanged -= Enemy_OnInBushChanged;
        enemy.OnTakeStep -= Enemy_OnTakeStep;
        enemy.OnStopChase -= Enemy_OnStopChase;
    }
}
