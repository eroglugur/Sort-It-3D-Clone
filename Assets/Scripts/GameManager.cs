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
    private UIManager uiManager;
    private InputManager inputManager;

    public bool isGameActive;
    private bool tubeIncreased;
    private bool celebrated;


    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        celebrated = false;
        
        levelManager = FindObjectOfType<LevelManager>();
        uiManager = FindObjectOfType<UIManager>();
        inputManager = FindObjectOfType<InputManager>().GetComponent<InputManager>();

        tubesToBeCompleted = PlayerPrefs.GetInt("LevelIndex") + 1;
        DetectTubes();

    }

    // Update is called once per frame
    void Update()
    {
        if (completedTubes == tubesToBeCompleted)
        {
            CelebrateWin();
            StartCoroutine("SetWinScreenActive");
        }
    }

    IEnumerator SetWinScreenActive()
    {
        yield return new WaitForSeconds(1.5f);
        uiManager.winScreen.SetActive(true);
        inputManager.enabled = false;
    }

    private void CelebrateWin()
    {
        if (!celebrated)
        {
            Instantiate(celebrationParticles);
            for (int i = 0; i < tubes.Count; i++)
            {
                tubes[i].GetComponent<TubeController>().enabled = true;
                StartCoroutine(tubes[i].GetComponent<TubeController>().ShakeTube());
                tubes[i].GetComponent<TubeController>().enabled = false;
            }
        }
        celebrated = true;
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
