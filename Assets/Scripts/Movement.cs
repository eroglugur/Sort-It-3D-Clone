using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private float ballRiseValueY = 11.0f;
    private float ballPlaceValueY = 11.0f;
    private float ballMoveTime = 0.5f;

    [SerializeField] private GameObject[] ball = new GameObject[1];

    private Movement movement;
    
    private void Start()
    {
        movement = GetComponent<Movement>();
        movement.enabled = false;
    }


    public void RiseBall(GameObject touchedObject)
    {
        Debug.Log("Touched " + touchedObject.transform.name);

        ball[0] = GetBall(touchedObject);

        ball[0].transform.DOMoveY(ballRiseValueY, ballMoveTime);
    }
    
    public void PlaceBall(GameObject touchedObject, GameObject ball)
    {
        Debug.Log("Touched " + touchedObject.transform.name);
        
        int ballCountInTube = touchedObject.GetComponent<TubeController>().CheckTubeElements();
        
        switch (ballCountInTube)
        {
            case 1:
                ballPlaceValueY = 1f;
                break;
            case 2:
                ballPlaceValueY = 3f;
                break;
            case 3:
                ballPlaceValueY = 5f;
                break;
            case 4:
                ballPlaceValueY = 7f;
                break;
        }
        
        ball.transform.DOMoveY(ballPlaceValueY, ballMoveTime);
    }

    public GameObject GetBall(GameObject touchedObject)
    {
        return touchedObject.GetComponent<TubeController>().GetLatestAddedBall();
    }

    public void SetDestination(GameObject touchedObject)
    {
        Vector3 direction = new Vector3(touchedObject.transform.position.x, ballRiseValueY, touchedObject.transform.position.z);

        ball[0].transform.DOMove(direction, ballMoveTime).OnComplete(() => PlaceBall(touchedObject, ball[0]));
        touchedObject.GetComponent<TubeController>().AddBall(ball[0]);
        
    }
}