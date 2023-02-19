using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Enemy : Unit
{
    const float BUSH_SPEED_MODIFIER = 0.25f;
    [SerializeField] List<Transform> patrolTransformList;
    [SerializeField, Range(3f, 30f)] float detectionRadius;
    [SerializeField, Range(3f, 60f)] float chaseRadius;
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] float attackCooldownTimerMax;

    bool isChasingPlayer;

    float attackCooldownTimer;
    float minDistance = 1f;
    [SerializeField] float attackRange = 6f;

    bool isAttacking;
    bool isTakingStep;

    Player player;
    EnemySlowInBush enemySlowInBush;

    float startingSpeed;
    float bushSpeed;
    float stepLength;

    public bool isInBush;
    bool hasGrowled;

    public event Action OnTakeStep;
    public event Action OnDetectMouse;
    public event Action<bool> OnInBushChanged;
    public event Action OnCatAttackStart;

    Transform targetTransform;
    Transform playerTransform;
    int patrolIndex = 0;



    void Start()
    {
        attackCooldownTimer = attackCooldownTimerMax;

        targetTransform = patrolTransformList[patrolIndex];

        player = MazePhaseManager.Instance.GetPlayer();
        playerTransform = player.transform;

        startingSpeed = moveSpeed;
        bushSpeed = startingSpeed * BUSH_SPEED_MODIFIER;

        enemySlowInBush = GetComponent<EnemySlowInBush>();
        enemySlowInBush.OnBushStatusChanged += EnemySlowInBush_OnBushStatusChanged;

        stepLength = GetComponent<EnemySounds>().walkSound.length;
        Debug.Log($"stepLength = {stepLength}");
    }

    private void EnemySlowInBush_OnBushStatusChanged(bool isInBush)
    {
        moveSpeed = isInBush ? bushSpeed : startingSpeed;

        if (this.isInBush != isInBush)
        {
            this.isInBush = isInBush;
            OnInBushChanged?.Invoke(isInBush);
            
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.up, detectionRadius);

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.up, chaseRadius);
    }
#endif

    protected override Vector3 CalculateMoveDirection()
    {
        if (isAttacking) return Vector3.zero;

        // get next waypoint from list
        Vector3 nextWaypoint = patrolTransformList[patrolIndex].position;
        float distanceToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint);
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRadius)
        {
            // start chasing
            targetTransform = playerTransform;
            isChasingPlayer = true;

            if (!hasGrowled)
            {
                // growl
                OnDetectMouse?.Invoke();
                hasGrowled = true;
            }

        }
        else if (isChasingPlayer && distanceToPlayer > chaseRadius)
        {
            // Stop Chasing
            targetTransform = patrolTransformList[patrolIndex];
            hasGrowled = false;
        }

        if (distanceToPlayer < attackRange)
        {
            // attack player
            Attack();
            return Vector3.zero;
        }

        if (distanceToNextWaypoint < minDistance)
        {
            // set next waypoint as target
            patrolIndex = (patrolIndex + 1) % patrolTransformList.Count;
            targetTransform = patrolTransformList[patrolIndex];
            return Vector3.zero;
        }

        Vector3 moveDirection = (targetTransform.position - transform.position).normalized;

        if (moveDirection != Vector3.zero & !isTakingStep)
        {
            CatTakeStep();
        }

        return moveDirection;
    }

    private void CatTakeStep()
    {
        OnTakeStep?.Invoke();
        StartCoroutine(nameof(StepCooldown));
    }

    IEnumerator StepCooldown()
    {
        print("enemyStep");
        isTakingStep = true;
        yield return new WaitForSeconds(stepLength);
        isTakingStep = false;
    }

    protected override Vector3 CalculateRotationDirection()
    {
        if (CalculateMoveDirection() != Vector3.zero)
        {
            return CalculateMoveDirection();
        }
        else
        {
            return transform.forward;
        }
    }

    void Attack()
    {
        isAttacking = true;
        attackCooldownTimer = attackCooldownTimerMax;

        animator.SetTrigger("Bite");
        player.TakeDamage();
        OnCatAttackStart?.Invoke();

        // TODO: Play Attack Particle Effect
        targetTransform = patrolTransformList[patrolIndex];

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTimerMax);

        isAttacking = false;
        attackCooldownTimer = attackCooldownTimerMax;
    }
}
