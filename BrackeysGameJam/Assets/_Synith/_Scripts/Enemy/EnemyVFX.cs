using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] Transform smokeVFXTransform;
    [SerializeField] Transform bushVFXTransform;
    [SerializeField] float smokeVFXDelay;

    Enemy enemy;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
    }

    void Start()
    {
        enemy.OnTakeStep += Enemy_OnTakeStep;
        enemy.OnCatAttackStart += Enemy_OnCatAttackStart;
    }

    void OnDestroy()
    {
        enemy.OnTakeStep -= Enemy_OnTakeStep;
        enemy.OnCatAttackStart -= Enemy_OnCatAttackStart;
    }

    void Enemy_OnCatAttackStart()
    {
        Invoke(nameof(SpawnSmokeEffect), smokeVFXDelay);
    }

    void Enemy_OnTakeStep()
    {
        SpawnBushEffect();
    }

    public void SpawnSmokeEffect() => Instantiate(smokeVFXTransform, transform.position, Quaternion.identity);
    public void SpawnBushEffect() => Instantiate(bushVFXTransform, transform.position, Quaternion.identity);
}
