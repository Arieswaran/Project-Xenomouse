using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField] Transform smokeVFXTransform;
    

    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        player.OnTakeDamage += Player_OnTakeDamage;
        
    }
    void OnDestroy()
    {
        player.OnTakeDamage -= Player_OnTakeDamage;
    }

    private void Player_OnTakeDamage()
    {
        SpawnSmokeEffect();
    }



    public void SpawnSmokeEffect() => Instantiate(smokeVFXTransform, transform.position, Quaternion.identity);
}
