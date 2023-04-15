using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScenesManager
{
    public static void LoadScene(Scene toLoad)
    {
        SceneManager.LoadScene(toLoad.ToString(), LoadSceneMode.Single);
    }
    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    public static void LoadGame()
    {
        SceneManager.LoadScene(Scene.Game.ToString(), LoadSceneMode.Single);
    }
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenu.ToString(), LoadSceneMode.Single);
    }
}

// the order and name of the scenes has to be the same of the Unity build
public enum Scene
{
    MainMenu,
    Game
}
