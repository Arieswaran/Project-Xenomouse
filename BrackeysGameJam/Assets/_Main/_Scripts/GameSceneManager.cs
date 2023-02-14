using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public enum Scene
    {
        Synith_MazePhase_Scene,
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}