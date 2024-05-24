using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    private static SceneManager.ScenesTypes cScene = SceneManager.ScenesTypes.menu;

    public static SceneManager.ScenesTypes currentScene
    {
        get { return cScene; }
        set { cScene = value; }
    }

    private static bool moveScenes = false;

    public static bool movingScenes
    {
        get { return moveScenes; }
        set { moveScenes = value; }
    }
}
