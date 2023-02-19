using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHider : MonoBehaviour
{
    [SerializeField] PatrollingEnemy[] patrollingEnemyListA;
    [SerializeField] PatrollingEnemy[] patrollingEnemyListB;

    private void Start()
    {
        int random = Random.Range(0, 2);

        Debug.Log($"random = {random}");

        PatrollingEnemy[] patrollingEnemyList = random == 0 ? patrollingEnemyListA : patrollingEnemyListB;
        HideEnemyPatrols(patrollingEnemyList);
    }

    void HideEnemyPatrols(PatrollingEnemy[] patrollingEnemyList)
    {
        foreach (PatrollingEnemy patrollingEnemy in patrollingEnemyList)
        {
            patrollingEnemy.Hide();
        }
    }

}
