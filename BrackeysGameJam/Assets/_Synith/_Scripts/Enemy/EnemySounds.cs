using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySounds : MonoBehaviour
{
    public AudioClip
    walkSound,
    bushSound,
    detectSound;

    AudioSource catAudio;
    Enemy enemy;


    void PlaySound(AudioClip clip) => catAudio.PlayOneShot(clip);

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
    }

    private void Enemy_OnTakeStep()
    {
        PlaySound(walkSound);
    }

    private void Enemy_OnInBushChanged(bool isInBush)
    {
        if (isInBush)
            PlaySound(bushSound);
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
    }
}
