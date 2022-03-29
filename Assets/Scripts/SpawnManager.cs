using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] allBalls = new GameObject[5];
    [SerializeField] private GameObject[] levelBalls;
    [SerializeField] private GameObject[] tubes;

    private LevelManager levelManager;
    private TubeController tubeController;

    private int ballTypes; // the number of ball types that will be spawned according to the level
    private int tubeCount; // the number of tubes that will be in the level
    private int tubeCapacity = 4;

    private int blue, red, yellow, green, orange = 0;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        tubeController = FindObjectOfType<TubeController>();
        levelManager = FindObjectOfType<LevelManager>();

        ballTypes = levelManager.levelIndex + 1;
        tubeCount = levelManager.levelIndex + 2;

        tubes = new GameObject[tubeCount];
        levelBalls = new GameObject[ballTypes];
    }

    void Start()
    {
        DetectBalls();
        DetectTubes();

        SpawnBalls();
    }

    // Detects the balls that will be spawned in the level
    private void DetectBalls()
    {
        for (int i = 0; i < levelBalls.Length; i++)
        {
            levelBalls[i] = allBalls[i];
        }
    }

    private void DetectTubes()
    {
        for (int i = 0; i < tubeCount; i++)
        {
            tubes[i] = GameObject.Find("Glass Tube " + (i + 1));
        }
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < ballTypes; i++)
        {
            for (int j = 0; j < tubeCapacity; j++)
            {
                int ballIndex = RandomizeBalls(ballTypes, tubes[i]);

                ControlTubes(ballIndex, tubes[i]);
                
                GameObject ballToSpawn = levelBalls[ballIndex];

                tubes[i].GetComponent<TubeController>().AddBall(ballToSpawn, GetSpawnPosition(tubes[i]));
            }
        }
    }

    private void ControlTubes(int ballIndex, GameObject tube)
    {
        if (tube.gameObject.GetComponent<TubeController>().HasMoreThanThreeSameColors())
        {
            levelManager.RestartScene();
        }
    }
    
    private Vector3 GetSpawnPosition(GameObject tube)
    {
        float ballSpawnPositionY = 0.5f;
        int ballCountInTube = tube.GetComponent<TubeController>().CheckTubeElements();

        switch (ballCountInTube)
        {
            case 0:
                ballSpawnPositionY = 0.5f;
                break;
            case 1:
                ballSpawnPositionY = 1.75f;
                break;
            case 2:
                ballSpawnPositionY = 3f;
                break;
            case 3:
                ballSpawnPositionY = 4.25f;
                break;
        }

        return SetSpawnPosition(tube, ballSpawnPositionY);
    }


    private Vector3 SetSpawnPosition(GameObject tube, float ballY)
    {
        return new Vector3(tube.transform.position.x, ballY, tube.transform.position.z);
    }

    private int RandomizeBalls(int ballCount, GameObject tube)
    {
        int returnValue = 0;
        int randomIndex = GenerateRandomIndex(ballCount, tube);

        switch (randomIndex)
        {
            case 0:
                returnValue = 0;
                blue++;
                break;
            case 1:
                returnValue = 1;
                red++;
                break;
            case 2:
                returnValue = 2;
                yellow++;
                break;
            case 3:
                returnValue = 3;
                green++;
                break;
            case 4:
                returnValue = 4;
                orange++;
                break;
        }

        return returnValue;
    }

    // Ensures that there are four balls of each color
    private int GenerateRandomIndex(int ballCount, GameObject tube)
    {
        int randomIndex = Random.Range(0, ballCount);

        if ((randomIndex == 0 && blue >= 4))
        {
            randomIndex = Random.Range(0, ballCount);
            while (randomIndex == 0)
            {
                randomIndex = Random.Range(0, ballCount);
            }
        }

        if (randomIndex == 1 && red >= 4)
        {
            randomIndex = Random.Range(0, ballCount);
            while (randomIndex == 1)
            {
                randomIndex = Random.Range(0, ballCount);
            }
        }

        if (randomIndex == 2 && yellow >= 4)
        {
            randomIndex = Random.Range(0, ballCount);
            while (randomIndex == 2)
            {
                randomIndex = Random.Range(0, ballCount);
            }
        }

        if (randomIndex == 3 && green >= 4)
        {
            randomIndex = Random.Range(0, ballCount);
            while (randomIndex == 3)
            {
                randomIndex = Random.Range(0, ballCount);
            }
        }

        if (randomIndex == 4 && orange >= 4)
        {
            randomIndex = Random.Range(0, ballCount);
            while (randomIndex == 4)
            {
                randomIndex = Random.Range(0, ballCount);
            }
        }

        return randomIndex;
    }
}