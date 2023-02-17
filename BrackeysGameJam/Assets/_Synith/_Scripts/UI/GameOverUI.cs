using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        MazePhaseManager.Instance.OnGameOver += MazePhaseManager_OnGameOver;
        Hide();
    }

    void OnDestroy()
    {
        MazePhaseManager.Instance.OnGameOver -= MazePhaseManager_OnGameOver;
    }

    void MazePhaseManager_OnGameOver()
    {
        Show();
    }
    void Show() => gameObject.SetActive(true);
    void Hide() => gameObject.SetActive(false);

}
