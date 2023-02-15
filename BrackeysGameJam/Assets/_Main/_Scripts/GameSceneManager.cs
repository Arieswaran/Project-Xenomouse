using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public enum Scene
    {
        RaisePhaseScene,
        MazePhaseScene,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}