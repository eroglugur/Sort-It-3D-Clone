using System;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BallController : MonoBehaviour
{
    private TubeController tubeController;
    private BallController ballController;
    private InputManager inputManager;

    private float ballRiseValueY = 12.0f;
    private float ballPlaceValueY = 12.0f;
    private float ballMoveTime = 0.25f;

    private bool shaked;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();

        ballController = GetComponent<BallController>();
        ballController.enabled = false;
    }

    public float SetPlaceValue(GameObject touchedObject)
    {
        int ballCountInTube = touchedObject.GetComponent<TubeController>().CheckTubeElements();

        switch (ballCountInTube)
        {
            case 0:
                ballPlaceValueY = 1f;
                break;
            case 1:
                ballPlaceValueY = 3f;
                break;
            case 2:
                ballPlaceValueY = 5f;
                break;
            case 3:
                ballPlaceValueY = 7f;
                break;
        }

        return ballPlaceValueY;
    }

    public void GoToTheTubeSelected(GameObject tubeSelected)
    {
        bool translationComplete = false;


        Vector3 destination = new Vector3(tubeSelected.transform.position.x, transform.position.y,
            tubeSelected.transform.position.z);

        ballPlaceValueY = SetPlaceValue(tubeSelected);

        transform.DOMove(destination, ballMoveTime)
            .OnComplete(() => transform.DOMoveY(ballPlaceValueY, ballMoveTime));
    }

    public void ShakeBall()
    {
        shaked = false;
        if (!shaked)
        {
            transform.DOShakePosition(0.5f, 0.50f, 25);
            shaked = true;
        }
    }

    public void ExitTube()
    {
        transform.DOMoveY(ballRiseValueY, ballMoveTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tube"))
        {
            tubeController = other.GetComponent<TubeController>();

            inputManager.isBallInTube = true;
            tubeController.AddBall(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Tube"))
        {
            tubeController = other.GetComponent<TubeController>();

            inputManager.isBallInTube = false;
            tubeController.RemoveBall();
        }
    }
}