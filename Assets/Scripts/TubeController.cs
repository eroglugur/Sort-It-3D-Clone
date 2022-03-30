using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TubeController : MonoBehaviour
{
    public List<GameObject> balls = new List<GameObject>();

    public bool isTubeFull;

    private int blue, red, yellow, green, orange = 0;

    void Update()
    {
        CheckIfTubeIsFull();
        
    }
    private void CheckIfTubeIsFull()
    {
        if (CheckTubeElements() >= 4)
        {
            isTubeFull = true;
        }
        else
        {
            isTubeFull = false;
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