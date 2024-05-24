using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSelector : MonoBehaviour
{
    [SerializeField]
    List<GameObject> canvasList = new List<GameObject>();

    SceneManager.ScenesTypes oldCanvas = SceneManager.ScenesTypes.menu;

    public void activateCanvas(SceneManager.ScenesTypes newCanvas)
    {
        canvasList[(int)oldCanvas].GetComponent<FadeInOut>().FadeOut(0.2f);

        canvasList[(int)newCanvas].SetActive(true);
        canvasList[(int)newCanvas].GetComponent<FadeInOut>().FadeIn(1.6f,0.2f);

        oldCanvas = newCanvas;
    }
}
