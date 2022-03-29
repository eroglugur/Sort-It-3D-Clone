using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TubeController : MonoBehaviour
{
    public GameObject[] balls = new GameObject[4];
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
        int sphereNumber = CheckTubeElements() - 1;
        return balls[sphereNumber];
    }

    // Removes the latest added ball from the array
    public void RemoveBall()
    {
        int sphereNumber = CheckTubeElements() - 1;
        balls[sphereNumber] = null;
    }

    // Adds the ball to the first empty element of the array
    public void AddBall(GameObject ball, Vector3 spawnPosition)
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

            int sphereNumber = CheckTubeElements();
            balls[sphereNumber] = Instantiate(ball, spawnPosition, Quaternion.identity);
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
        int sphereCount = 0;

        foreach (GameObject sphere in balls)
        {
            if (sphere == null)
            {
                return sphereCount;
            }
            else
            {
                sphereCount++;
            }
        }

        return sphereCount;
    }
}