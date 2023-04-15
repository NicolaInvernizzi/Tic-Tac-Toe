using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public void Pause()
    {
        MenusManager.EnableMenu(Menu.PAUSE, this.gameObject);
    }
}
