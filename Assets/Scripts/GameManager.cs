using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int completedTubes;
    public int tubesToBeCompleted;

    private LevelManager levelManager;

    private bool isGameActive;
    private bool isLevelWon;
    private bool tubeIncreased;

    [SerializeField] private List<GameObject> tubes = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        tubesToBeCompleted = levelManager.levelIndex + 1;
        DetectTubes();

    }

    // Update is called once per frame
    void Update()
    {
        if (completedTubes == tubesToBeCompleted)
        {
            isLevelWon = true;
            CelebrateWin();
        }

       
    }

    private void CelebrateWin()
    {
        for (int i = 0; i < tubes.Count; i++)
        {
            tubes[i].GetComponent<TubeController>().enabled = true;
            StartCoroutine(tubes[i].GetComponent<TubeController>().ShakeTube());
        }
    }

    public void UpdateTubeCount()
    {
        tubeIncreased = false;
        if (!tubeIncreased)
        {
            completedTubes++;
            tubeIncreased = true;
            Debug.Log(completedTubes);
        }
    }
    
    private void DetectTubes()
    {
        for (int i = 0; i < tubesToBeCompleted; i++)
        {
            tubes.Add(GameObject.Find("Glass Tube " + (i + 1)));
        }
    }
}
