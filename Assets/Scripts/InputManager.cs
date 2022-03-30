using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private bool touched;
    [SerializeField] private bool isUp;
    private bool isDestinationSet = false;

    [SerializeField] private GameObject[] touchedObject = new GameObject[1];

    private GameObject destinationObject;
    
    void Start()
    {
        bool isGameActive = true;
        while (isGameActive)
        {
            if (!touched && !isUp)
            {
                DetectTubeWithTouch();
            }

            if (touched && isUp && touchedObject[0] != null)
            {
                GameObject destinationObject = GetDestinationWithTouch();

                if (!destinationObject.GetComponent<TubeController>().isTubeFull && !isDestinationSet)
                {
                    touchedObject[0].GetComponent<Movement>().SetDestination(destinationObject);
                    touchedObject[0].GetComponent<TubeController>().RemoveBall();

                    isDestinationSet = true;
                    touched = false;
                    isUp = false;
                    touchedObject[0] = null;
                }

                DetectTubeWithTouch();
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
                    touchedObject[0] = hit.transform.gameObject;
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
        if (!touched && !isUp)
        {
            touchedObject[0].GetComponent<Movement>().enabled = true;

            touchedObject[0].GetComponent<Movement>().RiseBall(touchedObject[0]);
            touched = true;
            isUp = true;
        }
    }


    
}