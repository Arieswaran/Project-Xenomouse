using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSounds : MonoBehaviour
{
    public AudioClip
    scientestAnyDeathSound,
    mazeAmbience,
    mazeMusic;


    public AudioClip[] buttonClickSounds;


    void Start()
    {
        AudioClip startingNoise = buttonClickSounds[2];
        SoundEffects.Instance.PlayClip(startingNoise);
    }
}
