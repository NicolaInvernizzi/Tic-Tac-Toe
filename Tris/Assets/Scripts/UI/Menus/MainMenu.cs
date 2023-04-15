using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public void SinglePlayer()
    {
        GameMode.gameMode = Mode.Single;
        ScenesManager.LoadScene(Scene.Game);
    }
    public void MultiPlayer()
    {
        GameMode.gameMode = Mode.Multi;
        ScenesManager.LoadScene(Scene.Game);
    }

    public void Exit()
    {
        EditorApplication.isPlaying = false;
    }
}
