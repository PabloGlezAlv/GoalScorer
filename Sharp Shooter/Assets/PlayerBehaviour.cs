using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SceneManager;

public class PlayerBehaviour : MonoBehaviour
{

    [SerializeField]
    float timeMoving = 2;
    [SerializeField]
    List<GameObject> locations = new List<GameObject>();

    private float elapsedTime = 0f;
    bool moving = false;
    
    Vector3 startPosition = Vector3.zero;

    Vector3 targetPosition = Vector3.zero;

    private void Update()
    {
        if (moving)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / timeMoving;

            t = Mathf.Clamp01(t);

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
            {
                moving = false;
                PlayerData.movingScenes = false;
            }
        }
    }

    public void changeScene(ScenesTypes l)
    {

        moving = true;
        elapsedTime = 0f;
        startPosition = transform.position;
        targetPosition = locations[(int)l].transform.position;
    }

}
