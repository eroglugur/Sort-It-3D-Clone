using System;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Movement movement;
    private TubeController tubeController;
    
    private void Start()
    {
        movement = GetComponent<Movement>();
        tubeController = FindObjectOfType<TubeController>();
    }

   private void Update()
    {
        if (transform.position.y == 4.25f)
        {
            movement.enabled = true;
        }
        else
        {
            movement.enabled = false;
        }
    }

}
