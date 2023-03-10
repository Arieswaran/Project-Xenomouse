using System;
using UnityEngine;

public class MazePhaseManager : MonoBehaviour
{
    [SerializeField] Player player;
    public event Action OnGameOver;

  
    public static MazePhaseManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        Player.OnMouseDeath += Player_OnMouseDeath;
    }

    public bool TryApplyStatsFromLastGeneration()
    {
        if (GameManager.instance == null) return false;

        MouseData mouseData = GameManager.instance.GetMouseData();
        MouseMazeStats mouseStats = new(mouseData.health, mouseData.speed, mouseData.lifespan);
        player.SetStats(mouseStats);
        return true;
    }

    private void OnDestroy()
    {
        Player.OnMouseDeath -= Player_OnMouseDeath;
    }

    private void Player_OnMouseDeath()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.GenerateNextGenerationMouse();
    }

    public void IncreaseRedCheeseCount()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.IncreaseRedCheese();
    }
    public void IncreaseGreenCheeseCount()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.IncreaseGreenCheese();
    }
    public void IncreaseBlueCheeseCount()
    {
        if (GameManager.instance == null) return;
        GameManager.instance.IncreaseBlueCheese();
    }

    public void WinGame()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 0;
        Debug.Log("YOU WIN!!!!");
    }

    public Player GetPlayer() => player;
}
