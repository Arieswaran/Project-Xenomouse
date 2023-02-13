using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float rotationSpeed = 900f;
    [SerializeField] int maxHealth = 100;


    Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //rb.ResetInertiaTensor();

        HandleMovement();
    }


    void HandleMovement()
    {
        Vector3 moveDirection = CalculateMoveDirection();
        Vector3 rotationDirection = CalculateRotationDirection();

        if (moveDirection != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(rotationDirection);
            Vector3 moveVector = transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rotation);
            rb.MovePosition(moveVector);            
        }
    }

    protected abstract Vector3 CalculateMoveDirection();
    protected abstract Vector3 CalculateRotationDirection();
}

