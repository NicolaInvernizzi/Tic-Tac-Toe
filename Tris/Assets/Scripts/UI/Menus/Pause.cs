using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void Exit()
    {
        ScenesManager.LoadScene(Scene.MainMenu);
    }

    public void Continue()
    {
        MenusManager.EnableMenu(Menu.GAME, this.gameObject);
    }
}
