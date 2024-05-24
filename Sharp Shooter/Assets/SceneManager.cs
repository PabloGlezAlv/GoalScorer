using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public enum ScenesTypes { menu, game }


    [SerializeField]
    PlayerBehaviour playerBehaviour;
    [SerializeField]
    PlayerBehaviour cameraBehaviour;
    [SerializeField]
    CanvasSelector canvasSelector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            ChangeLocation(ScenesTypes.game);
        }
    }

    public void ChangeLocation(ScenesTypes l)
    {
        if (PlayerData.currentScene == l) return;

        playerBehaviour.changeScene(l);
        cameraBehaviour.changeScene(l);
        canvasSelector.activateCanvas(l);
        PlayerData.currentScene = l;
        PlayerData.movingScenes = true;
    }
}
