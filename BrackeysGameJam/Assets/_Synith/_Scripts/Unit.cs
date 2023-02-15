using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 900f;
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected Animator animator;

    Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //rb.ResetInertiaTensor();
        HandleMovement();
        HandleAnimation();
    }

    void HandleAnimation()
    {
        if (CalculateMoveDirection() != Vector3.zero)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void HandleMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        Vector3 moveDirection = CalculateMoveDirection();
        Vector3 rotationDirection = CalculateRotationDirection();

        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(rotationDirection);
            Vector3 moveVector = transform.position + (moveDirection * moveSpeed);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rotation);
            rb.MovePosition(Vector3.Lerp(transform.position, moveVector, Time.fixedDeltaTime));            
        }
    }

    protected abstract Vector3 CalculateMoveDirection();
    protected abstract Vector3 CalculateRotationDirection();
}

