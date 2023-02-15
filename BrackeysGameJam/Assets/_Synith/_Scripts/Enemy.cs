using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Synith
{
    public class Enemy : Unit
    {
        [SerializeField] List<Transform> patrolTransformList;
        [SerializeField, Range(3f, 30f)] float detectionRadius;
        [SerializeField] LayerMask detectionLayer;
        [SerializeField] float attackCooldownTimerMax;

        float attackCooldownTimer;
        float minDistance = 1f;
        float attackRange = 3f;

        public bool isAttacking;

        Player player;

        Transform targetTransform;
        Transform playerTransform;
        int patrolIndex = 0;

        private void Start()
        {
            attackCooldownTimer = attackCooldownTimerMax;

            targetTransform = patrolTransformList[patrolIndex];

            player = FindObjectOfType<Player>();
            playerTransform = player.transform;
            Debug.Log($"Found Player: {player.name}");
        }


        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, transform.up, detectionRadius);
        }

        protected override Vector3 CalculateMoveDirection()
        {
            if (isAttacking) return Vector3.zero;

            Vector3 nextWaypoint = patrolTransformList[patrolIndex].position;
            float distanceToNextWaypoint = Vector3.Distance(transform.position, nextWaypoint);
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < detectionRadius)
            {
                targetTransform = playerTransform;
            }
            else
            {
                targetTransform = patrolTransformList[patrolIndex];
            }

            if (distanceToPlayer < attackRange)
            {
                Debug.Log("Attacking Player!");
                Attack();
                return Vector3.zero;
            }

            if (distanceToNextWaypoint < minDistance)
            {
                Debug.Log("Arrived at waypoint!");
                patrolIndex = (patrolIndex + 1) % patrolTransformList.Count;
                targetTransform = patrolTransformList[patrolIndex];
                return Vector3.zero;
            }

            Vector3 moveDirection = (targetTransform.position - transform.position).normalized;
            return moveDirection;
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

            player.TakeDamage();
            targetTransform = patrolTransformList[patrolIndex];

            StartCoroutine(AttackCooldown2());
        }

        IEnumerator AttackCooldown2()
        {
            yield return new WaitForSeconds(attackCooldownTimerMax);

            isAttacking = false;
            attackCooldownTimer = attackCooldownTimerMax;
            Debug.Log($"ATTACK COOLDOWN {attackCooldownTimer}");

        }
    }
}