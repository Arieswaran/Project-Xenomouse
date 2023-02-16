using UnityEngine;

public class MazePhaseManager : MonoBehaviour
{
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
        if (GameManager.instance != null)
        {
            GameManager.instance.GenerateNextGenerationMouse();
        }
    }
}
