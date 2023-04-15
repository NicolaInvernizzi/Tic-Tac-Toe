using UnityEngine;

public static class MenusManager
{
    internal static GameObject gameMenu, pauseMenu;
    public static bool enable { get; private set; } // unused; how can i use it ? where can i set it to false (without a private setter) ?
    private static void GetMenus()
    {
        GameObject canvas = GameObject.Find("Canvas");
        gameMenu = canvas.transform.Find("GameMenu").gameObject;
        pauseMenu = canvas.transform.Find("PauseMenu").gameObject;
        enable = true;
    }
    public static void EnableMenu(Menu toEnable, GameObject toDisable)
    {
        if (gameMenu == null)
            GetMenus();

        switch(toEnable)
        {
            case Menu.GAME:
                gameMenu.SetActive(true);
                break;
            case Menu.PAUSE:
                pauseMenu.SetActive(true);
                break;
        }
        toDisable.SetActive(false);
    }
}

public enum Menu
{
    GAME,
    PAUSE
}
