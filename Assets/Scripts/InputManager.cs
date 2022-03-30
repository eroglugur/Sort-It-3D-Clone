using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isBallInTube;

    private GameObject touchedGameObject;
    [SerializeField] private GameObject[] ball = new GameObject[1];

    private BallController ballController;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        isBallInTube = true;
    }

    void Update()
    {
        GetTouch();
    }

    private void GetTouch()
    {
        if (gameManager.isGameActive)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.CompareTag("Tube"))
                {
                    touchedGameObject = hit.collider.gameObject;

                    if (isBallInTube)
                    {
                        ball[0] = touchedGameObject.GetComponent<TubeController>().GetLatestAddedBall();
                    }

                    if (touchedGameObject.GetComponent<TubeController>().isTubeFull && !isBallInTube)
                    {
                        ball[0].GetComponent<BallController>().ShakeBall();
                    }
                    else
                    {
                        ProcessTouch(touchedGameObject, ball[0]);
                    }
                }
            }
        }
    }

    private void ProcessTouch(GameObject tube, GameObject ballSelected)
    {
        ballController = ballSelected.GetComponent<BallController>();

        if (isBallInTube)
        {
            ballController.enabled = true;
            ballController.ExitTube();
        }
        else
        {
            ballController.enabled = false;
            ballController.GoToTheTubeSelected(tube);
            ball[0] = null;
        }
    }
}