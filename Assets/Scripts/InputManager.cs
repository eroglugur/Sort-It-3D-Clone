using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool touched = false;
    [SerializeField] private bool isUp = false;
    public bool isDestinationSet = false;

    [SerializeField] private GameObject[] tube = new GameObject[1];

    private GameObject destinationObject;

    void Update()
    {
        if (!touched && !isUp)
        {
            isDestinationSet = false;
            DetectTubeWithTouch();
        }

        if (touched && isUp && tube[0] != null)
        {
            GameObject destinationObject = GetDestinationWithTouch();

            if (!destinationObject.GetComponent<TubeController>().isTubeFull && !isDestinationSet)
            {
                tube[0].GetComponent<Movement>().SetDestination(destinationObject);

                tube[0] = null;
                isUp = false;
                touched = false;
                isDestinationSet = true;
            }
        }
    }

    void DetectTubeWithTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tube"))
                {
                    tube[0] = hit.transform.gameObject;
                    EnableMovementToTouchedObject();
                }
            }
        }
    }

    private GameObject GetDestinationWithTouch()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Tube"))
                {
                    destinationObject = hit.transform.gameObject;
                    return destinationObject;
                }
            }
        }

        return destinationObject;
    }


    private void EnableMovementToTouchedObject()
    {
        tube[0].GetComponent<Movement>().enabled = true;

        tube[0].GetComponent<Movement>().RiseBall(tube[0]);

        touched = true;
        isUp = true;
    }
}