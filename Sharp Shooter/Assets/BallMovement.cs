using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    GameObject lineRotator;
    [SerializeField]
    LineRenderer lineRenderer;
    [SerializeField]
    ScoreManager scoreManager;
    [SerializeField]
    float rotationSpeed = 100;
    [SerializeField]
    float powerSpeed = 100;
    [SerializeField]
    float ballTime = 10;

    bool inputStarted = false;
    int touchCount = 0;

    bool rotationDone = false;
    bool powerDone = false;

    bool ballRunning = false;

    Vector3 ballDir = Vector3.zero;
    float ballForce = 0;

    private List<Transform> linePoint = new List<Transform>();
    private List<Vector3> startPositions = new List<Vector3>();

    float timer = 0;

    private bool locked = false;

    Vector3 initPosition;

    Rigidbody2D rb;

    private void Awake()
    {
        initPosition = transform.position;

        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        foreach (Transform child in lineRotator.transform)
        {
            // Agregar el hijo a la lista
            linePoint.Add(child);
        }

        for (int i = 0; i < linePoint.Count; i++)
        {
            startPositions.Add(linePoint[i].position);
        }

        lineRenderer.positionCount = linePoint.Count;
        setLinePositions();

        lineRenderer.enabled = false;
    }

    public void lockControls(bool locked)
    {
        this.locked = locked;
    }

    private void setLinePositions()
    {
        for (int i = 0; i < linePoint.Count; i++)
        {
            lineRenderer.SetPosition(i, linePoint[i].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballRunning && !locked && PlayerData.currentScene == SceneManager.ScenesTypes.game && !PlayerData.movingScenes)
        {
            rb.velocity = Vector2.zero;
            if (Input.touchCount > 0 && touchCount == 0)
            {
                touchCount = Input.touchCount;
                inputStarted = true;
            }
            if (Input.GetMouseButtonDown(0) && touchCount == 0)
            {
                touchCount = 1;
                inputStarted = true;
            }

            if (touchCount > 0 && inputStarted)
            {
                if(!Input.GetMouseButton(0) || Input.touchCount < 0)
                {
                    touchCount = 0;
                    inputStarted = false;

                    if(!rotationDone)
                    {
                        rotationDone = true;
                        ballDir = (linePoint[1].position - linePoint[0].position).normalized;
                        //Debug.Log("rotacin elegida");
                    }
                    else if(!powerDone)
                    {
                        ballForce = Vector3.Distance(linePoint[1].transform.position, linePoint[0].transform.position);
                        powerDone = true;
                        //Debug.Log("fuerza elegida");
                    }
                }
            }

        
            if (inputStarted && !rotationDone)
            {
                if (!lineRenderer.enabled) lineRenderer.enabled = true;

                lineRotator.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);

                if (lineRotator.transform.rotation.eulerAngles.z > 180)
                {
                    lineRotator.transform.rotation = Quaternion.identity;
                    rotationSpeed = -rotationSpeed;
                }
                else if (lineRotator.transform.rotation.eulerAngles.z > 90)
                {
                    Vector3 currentRotation = lineRotator.transform.rotation.eulerAngles;
                    currentRotation.z = 90;
                    lineRotator.transform.rotation = Quaternion.Euler(currentRotation);

                    rotationSpeed = -rotationSpeed;
                }
                else 
                setLinePositions();
            }
            else if (inputStarted && !powerDone)
            {
                linePoint[1].position += ballDir * powerSpeed * Time.deltaTime;

                float dist = Vector3.Distance(linePoint[1].transform.position, linePoint[0].transform.position);

                if (dist > 1.5 || dist < 0.1)
                {
                    powerSpeed = -powerSpeed;
                }


                lineRenderer.SetPosition(1, linePoint[1].position);
            }
        }
        else if(ballRunning)
        {
            timer += Time.deltaTime;

            if(timer > ballTime)
            {
                ResetBall(false);

            }
        }
    }

    public void ResetBall(bool goal)
    {
        timer = 0;

        rotationSpeed = Mathf.Abs(rotationSpeed);
        powerSpeed = Mathf.Abs(powerSpeed);

        transform.position = initPosition;
        transform.rotation = Quaternion.identity;

        lineRotator.transform.rotation = Quaternion.identity;
        for (int i = 0; i < lineRotator.transform.childCount; i++)
        {
            lineRotator.transform.GetChild(i).transform.position = startPositions[i];
        }

        setLinePositions();

        ballRunning = false;
        rb.velocity = Vector2.zero;

        if(!goal)
        {
            scoreManager.ResetGame();
        }
    }

    public void addTimeGame()
    {  
        timer -= 10;
    }

    void FixedUpdate()
    {
        if(rotationDone && powerDone)
        {
            lineRenderer.enabled = false;
            rb.AddForce(ballDir * ballForce * 10, ForceMode2D.Impulse);

            rotationDone = false;
            powerDone = false;

            ballRunning = true;
        }
    }
}
