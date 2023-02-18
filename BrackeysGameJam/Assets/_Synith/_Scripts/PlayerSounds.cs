using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public
    AudioClip
    walkSound,
    eatSound,
    takeDamageSound,
    deathCatSound,
    deathTimerSound,
    fiveSecondsSound,
    happyWinSound;

    Player player;
    SoundEffects soundEffects;




    void Player_OnWin()
    {
        soundEffects.PlayClip(happyWinSound);
    }

    void Player_OnTimerAlmostFinished()
    {
        soundEffects.PlayClip(fiveSecondsSound);
    }

    void Player_OnMouseDeathFromTimer()
    {
        soundEffects.PlayClip(deathTimerSound);
    }

    void Player_OnMouseDeathFromCat()
    {
        soundEffects.PlayClip(deathCatSound);
    }

    void Player_OnTakeDamage()
    {
        soundEffects.PlayClip(takeDamageSound);
    }

    void Player_OnTakeStep()
    {
        soundEffects.PlayClip(walkSound);
    }

    void Player_OnEat()
    {
        soundEffects.PlayClip(eatSound);
    }

    void Start()
    {
        player = GetComponent<Player>();
        soundEffects = SoundEffects.Instance;

        player.OnEat += Player_OnEat;
        player.OnTakeStep += Player_OnTakeStep;
        player.OnTakeDamage += Player_OnTakeDamage;
        player.OnMouseDeathFromCat += Player_OnMouseDeathFromCat;
        player.OnMouseDeathFromTimer += Player_OnMouseDeathFromTimer;
        player.OnTimerAlmostFinished += Player_OnTimerAlmostFinished;
        player.OnWin += Player_OnWin;
    }

    void OnDestroy()
    {
        player.OnEat -= Player_OnEat;
        player.OnTakeStep -= Player_OnTakeStep;
        player.OnTakeDamage -= Player_OnTakeDamage;
        player.OnMouseDeathFromCat -= Player_OnMouseDeathFromCat;
        player.OnMouseDeathFromTimer -= Player_OnMouseDeathFromTimer;
        player.OnTimerAlmostFinished -= Player_OnTimerAlmostFinished;
        player.OnWin -= Player_OnWin;
    }
}
