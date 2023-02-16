using UnityEngine;

public class MazePhaseManager : MonoBehaviour
{
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
}
