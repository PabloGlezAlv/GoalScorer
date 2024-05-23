using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    ScoreManager scoreManager;

    [SerializeField]
    float confirmGoalTimer = 1.5f;

    bool score = false;

    BallMovement ball;
    float timer = 0;

    GoalMovement goalMovement;

    private void Awake()
    {
        goalMovement = transform.parent.GetComponent<GoalMovement>();
    }

    void Update()
    {
        if(score)
        {
            timer += Time.deltaTime;
            if(timer > confirmGoalTimer)
            {
                ball.ResetBall(true);
                timer = 0;
                score = false;
                goalMovement.changePosition();
            }
        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ball = collision.GetComponent<BallMovement>())
        {
            ball.addTimeGame(); //Make sure ball stays there
            ball.lockControls(true);
            score = true;
            scoreManager.AddScore(1);
        }
    }
}
