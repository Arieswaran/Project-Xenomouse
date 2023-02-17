using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;
    void Start()
    {
        SetupStartButton();
        SetupQuitButton();
    }

    void SetupStartButton()
    {
        if (playButton == null) return;
        playButton.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.RaisePhaseScene);
        });
    }

    void SetupQuitButton()
    {
        if (quitButton == null) return;
        quitButton.onClick.AddListener(() =>
        {
            QuitGame();
        });
    }


    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif  
    }
}
