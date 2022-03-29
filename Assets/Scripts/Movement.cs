using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private TubeController tubeController;
    private SpawnManager spawnManager;
    private GameObject ball;

    private float ballRiseValueY = 11.0f;
    private float ballRiseTime = 0.5f;

    private float ballPosY;
    [SerializeField] private bool touched;
    [SerializeField] private bool isUp = false;

    TouchPhase touchPhase = TouchPhase.Ended;

    private void Start()
    {
        DOTween.Init();
        tubeController = GetComponent<TubeController>();
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.transform.name);

                if (hit.collider != null)
                {
                    GameObject touchedObject = hit.transform.gameObject;
                    Debug.Log("Touched " + touchedObject.transform.name);
                }
            }
        }
        
        if (!touched)
        {
            ball = tubeController.GetLatestAddedBall();
            SetTouchedBoolValue(true);
        }

        if (!isUp)
        {
            RiseBall();
            SetIsUpBoolValue(true);
        }

    }

    private void RiseBall()
    {
        if (!touched)
        {
            ballPosY = ball.gameObject.transform.position.y;
            SetTouchedBoolValue(true);
        }

        ball.transform.DOMoveY(ballRiseValueY, ballRiseTime);
    }

    private void SetTouchedBoolValue(bool value)
    {
        foreach (var tube in spawnManager.tubes)
        {
            tube.GetComponent<Movement>().touched = value;
        }
    }

    private void SetIsUpBoolValue(bool value)
    {
        foreach (var tube in spawnManager.tubes)
        {
            tube.GetComponent<Movement>().isUp = value;
        }
    }
}