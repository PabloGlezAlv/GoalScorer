using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalMovement : MonoBehaviour
{
    [SerializeField]
    BallMovement ball;

    [SerializeField]
    float minHeight;
    [SerializeField]
    float maxHeight;

    bool move = false;

    Vector3 targetPosition = Vector3.zero;

    public void changePosition()
    {
        move = true;

        targetPosition = new Vector3(transform.position.x,UnityEngine.Random.Range(minHeight, maxHeight), transform.position.z);
    }

    private void Update()
    {
        if (move)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

            float dist = Vector3.Distance(transform.position, targetPosition);
            if (dist < 0.05)
            {
                move = false;
            }
            else if(dist < 0.5)
            {
                ball.lockControls(false);
            }
        }
    }
}
