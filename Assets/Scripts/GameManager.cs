using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> tubes = new List<GameObject>();
    [SerializeField] private GameObject celebrationParticles;
    
    public int completedTubes;
    public int tubesToBeCompleted;

    private LevelManager levelManager;

    public bool isGameActive;
    private bool tubeIncreased;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        levelManager = FindObjectOfType<LevelManager>();

        tubesToBeCompleted = levelManager.levelIndex + 1;
        DetectTubes();

    }

    // Update is called once per frame
    void Update()
    {
        if (completedTubes == tubesToBeCompleted)
        {
            CelebrateWin();
        }
    }

    private void CelebrateWin()
    {
        Instantiate(celebrationParticles);
        for (int i = 0; i < tubes.Count; i++)
        {
            tubes[i].GetComponent<TubeController>().enabled = true;
            StartCoroutine(tubes[i].GetComponent<TubeController>().ShakeTube());
            tubes[i].GetComponent<TubeController>().enabled = false;
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
