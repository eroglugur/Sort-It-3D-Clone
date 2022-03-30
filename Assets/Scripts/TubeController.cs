using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class TubeController : MonoBehaviour
{
    public List<GameObject> balls = new List<GameObject>();

    private GameManager gameManager;
    private TubeController tubeController;
    private BoxCollider boxCollider;
    
    public bool isTubeFull;
    public bool hasSameColors;

    private int blue, red, yellow, green, orange = 0;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        tubeController = GetComponent<TubeController>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        CheckIfTubeFullness();

        if (isTubeFull && hasSameColors)
        {
            gameManager.UpdateTubeCount();

            StartCoroutine("ShakeTube");
            
            tubeController.enabled = false;
            boxCollider.enabled = false;
        }
        
    }

    public IEnumerator ShakeTube()
    {
        yield return new WaitForSeconds(0.25f);
        transform.DOJump(transform.position, 1.2f, 3, 1.5f);
        foreach (GameObject ball in balls)
        {
            yield return new WaitForSeconds(0.02f);
            ball.transform.DOJump(ball.transform.position, 1.2f, 3, 1.5f);
        }
    }

    private void CheckIfTubeFullness()
    {
        if (CheckTubeElements() >= 4)
        {
            isTubeFull = true;
        }
        else
        {
            isTubeFull = false;
        }

        int colorCounter = 0;
        
        for (int i = 0; i < balls.Count -1; i++)
        {
            if (balls[i].name == balls[i + 1].name)
            {
                colorCounter++;
            }
        }

        if (colorCounter == 3)
        {
            hasSameColors = true;
        }
        else
        {
            hasSameColors = false;
        }
    }

    public GameObject GetLatestAddedBall()
    {
        GameObject latestAddedBall;
        if (balls.Count - 1 >= 0)
        {
            latestAddedBall = balls[balls.Count - 1];
        }
        else
        {
            latestAddedBall = null;
        }
        return latestAddedBall;
    }

    // Removes the latest added ball from the array
    public void RemoveBall()
    {
       
            balls.Remove(balls[balls.Count - 1]);
        
    }

    // Spawns the ball to the first empty elements of the array
    public void SpawnBallsAtStart(GameObject ball, Vector3 spawnPosition)
    {
        if (!isTubeFull)
        {
            switch (ball.name)
            {
                case "Blue Ball":
                    blue++;
                    break;
                case "Red Ball":
                    red++;
                    break;
                case "Yellow Ball":
                    yellow++;
                    break;
                case "Green Ball":
                    green++;
                    break;
                case "Orange Ball":
                    orange++;
                    break;
            }

            balls.Add(Instantiate(ball, spawnPosition, Quaternion.identity));
        }
    }

    // Adds the ball to the first empty element of the array
    public void AddBall(GameObject ball)
    {
        if (!isTubeFull)
        {
            switch (ball.name)
            {
                case "Blue Ball":
                    blue++;
                    break;
                case "Red Ball":
                    red++;
                    break;
                case "Yellow Ball":
                    yellow++;
                    break;
                case "Green Ball":
                    green++;
                    break;
                case "Orange Ball":
                    orange++;
                    break;
            }

            balls.Add(ball);
        }
    }

    public bool HasMoreThanThreeSameColors()
    {
        if (blue >= 3 || red >= 3 || yellow >= 3 || green >= 3 || orange >= 3)
        {
            return true;
        }

        return false;
    }

    // Checks the number of game objects in the array that are not null
    public int CheckTubeElements()
    {
        return balls.Count;
    }
}