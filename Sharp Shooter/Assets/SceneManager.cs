using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public enum ScenesTypes { menu, game }


    [SerializeField]
    PlayerBehaviour playerBehaviour;
    [SerializeField]
    PlayerBehaviour cameraBehaviour;
    [SerializeField]
    CanvasSelector canvasSelector;
    [SerializeField]
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    public void ChangeLocation(ScenesTypes l)
    {
        if (PlayerData.currentScene == l && !PlayerData.movingScenes) return;

        playerBehaviour.changeScene(l);
        cameraBehaviour.changeScene(l);
        canvasSelector.activateCanvas(l);
        PlayerData.currentScene = l;
        PlayerData.movingScenes = true;
    }

    public void goGame()
    {
        ChangeLocation(ScenesTypes.game);
    }
    public void goMenu()
    {
        scoreManager.ResetScore();
        ChangeLocation(ScenesTypes.menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
