using System;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    [SerializeField] private GameObject[] spheres = new GameObject[4];

    private int blue, red, yellow, green, orange = 0;

    // Removes the latest added ball from the array
    public void RemoveBall()
    {
        int sphereNumber = CheckTubeElements() - 1;
        spheres[sphereNumber] = null;
        Debug.Log(CheckTubeElements());
    }

    // Adds the ball to the first empty element of the array
    public void AddBall(GameObject ball)
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
        spheres[sphereNumber] = ball;
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

        foreach (GameObject sphere in spheres)
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