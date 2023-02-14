using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseStats
{
    public MouseStats(int health, int speed, int lifeSpan)
    {
        Health = health;
        Speed = speed;
        LifeSpan = lifeSpan;
    }

    public int Health { get; set; }
    public int Speed { get; set; }
    public int LifeSpan { get; set; }
}

public class Player : Unit
{
    PlayerInputActions playerInputActions;
    MouseStats mouseStats;
    float startingMoveSpeed;
    bool isDead;


    public void SetStats(int health, int speed, int lifeSpanSeconds)
    {
        mouseStats = new(health, speed, lifeSpanSeconds);
        UpdateStats();
        StartCoroutine(MouseLifeFading());
    }

    public void SetStats(MouseStats mouseStats)
    {
        this.mouseStats = mouseStats;
        UpdateStats();
        StartCoroutine(MouseLifeFading());
    }

    void UpdateStats()
    {
        maxHealth = mouseStats.Health;
        moveSpeed = startingMoveSpeed + mouseStats.Speed * 0.1f;
    }

    IEnumerator MouseLifeFading()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);

            mouseStats.LifeSpan--;

            Debug.Log(mouseStats.LifeSpan);

            if (mouseStats.LifeSpan <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true;
        StopCoroutine(MouseLifeFading());
        Debug.Log("BARARLGHGHGHH!!! IM DEAD");
        // TODO: PLAY DEATH ANIMATION
        //animator.SetTrigger("Death");
        animator.Play("Death");
    }

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new();
        startingMoveSpeed = moveSpeed;

        // Set default mouse stats
        SetStats(new(2, 5, 14));
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
        Debug.Log($"ate {cheese.cheeseType}");
        switch (cheese.cheeseType)
        {
            case Cheese.CheeseType.Red:
                break;
            case Cheese.CheeseType.Green:
                break;
            case Cheese.CheeseType.Blue:
                break;
        }

        animator.SetTrigger("Eat");
        Destroy(cheese.gameObject);
    }

    public void TakeDamage()
    {
        Debug.Log("Ouch!");
        animator.SetTrigger("Hurt");
    }
}
