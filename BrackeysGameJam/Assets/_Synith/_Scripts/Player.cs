using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : Unit
{
    PlayerInputActions playerInputActions;
    MouseMazeStats mouseStats;
    float startingMoveSpeed;
    int lifeSpan;
    bool isDead;

    public static event Action OnMouseDeath;

    public void SetStats(int health, int speed, int lifeSpanSeconds)
    {
        mouseStats = new(health, speed, lifeSpanSeconds);
        UpdateStats();
        StartCoroutine(MouseLifeFading());
    }

    public void SetStats(MouseMazeStats mouseStats)
    {
        this.mouseStats = mouseStats;
        UpdateStats();
        StartCoroutine(MouseLifeFading());
    }

    void UpdateStats()
    {
        maxHealth = mouseStats.Health;
        moveSpeed = startingMoveSpeed * (1 + mouseStats.Speed * 0.1f);
        lifeSpan = mouseStats.LifeSpan;
    }

    IEnumerator MouseLifeFading()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);

            lifeSpan--;

            //Debug.Log(mouseStats.LifeSpan);

            if (lifeSpan <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        StopCoroutine(MouseLifeFading());
        animator.SetTrigger("Death");

        OnMouseDeath?.Invoke();
    }

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new();
        startingMoveSpeed = moveSpeed;


        if (!MazePhaseManager.Instance.TryApplyStatsFromLastGeneration())
        {
            // if there is no game manager use testing stats
            SetStats(new(2, 5, 14));
        }        
    }

    void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    protected override Vector3 CalculateMoveDirection()
    {
        if (isDead) return Vector3.zero;

        InputAction moveAction = playerInputActions.Player.Move;
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = Vector3.zero;

        moveDirection += Vector3.ProjectOnPlane(Camera.main.transform.right, transform.up).normalized * input.x;
        moveDirection += Vector3.ProjectOnPlane(Camera.main.transform.forward, transform.up).normalized * input.y;

        return moveDirection;
    }

    protected override Vector3 CalculateRotationDirection()
    {
        if (CalculateMoveDirection() != Vector3.zero)
        {
            Vector3 rotationDirectionFromMovement = CalculateMoveDirection();
            return rotationDirectionFromMovement;
        }
        else
        {
            return transform.forward;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Cheese cheese))
        {
            EatCheese(cheese);
        }
    }

    void EatCheese(Cheese cheese)
    {
        cheese.Consume();
        animator.SetTrigger("Eat");
        Destroy(cheese.gameObject);
    }

    public void TakeDamage()
    {
        if (isDead) return;

        mouseStats.Health--;

        Debug.Log("Health Remaining:" + mouseStats.Health);

        if (mouseStats.Health <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Ouch!");
            animator.SetTrigger("Hurt");
        }
        
    }
}
