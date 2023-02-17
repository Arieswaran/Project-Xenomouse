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
    float buffedMoveSpeed;
    public int lifeSpan;
    bool isDead;
    [SerializeField] bool debugMode;
    [SerializeField] LayerMask bushLayerMask;
    [SerializeField] float collisionDistance = 1.5f;

    public static event Action OnMouseDeath;
    public event Action<int> OnHealthAmountChanged;
    public event Action<int> OnLifespanChanged;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + Vector3.up, transform.position + transform.forward * collisionDistance);
    }

    public int GetLifeSpan() => lifeSpan;
    public int GetHealth() => mouseStats.Health;

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
        buffedMoveSpeed = moveSpeed;
        lifeSpan = mouseStats.LifeSpan;
    }

    IEnumerator MouseLifeFading()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(1f);

            lifeSpan--;
            OnLifespanChanged?.Invoke(lifeSpan);

            if (lifeSpan <= 0)
            {
                lifeSpan = 0;
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
            MouseMazeStats defaultStats = new(20, 5, 14);
            MouseMazeStats godModeStats = new(99, 50, 999);

            MouseMazeStats startingStats = debugMode ? godModeStats : defaultStats;
            SetStats(startingStats);
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

        Ray ray = new(transform.position + Vector3.up, transform.forward * collisionDistance);

        if (Physics.Raycast(ray, collisionDistance, bushLayerMask))
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = buffedMoveSpeed;
        }

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
        if (other.TryGetComponent(out ExitZone exitZone))
        {
            exitZone.WinGame();
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

        mouseStats.Health -= 10;       


        Debug.Log("Health Remaining:" + mouseStats.Health);

        if (mouseStats.Health <= 0)
        {
            mouseStats.Health = 0;
            Die();
        }
        else
        {
            Debug.Log("Ouch!");
            animator.SetTrigger("Hurt");
        }
        OnHealthAmountChanged?.Invoke(mouseStats.Health);
    }
}
