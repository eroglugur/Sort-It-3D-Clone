using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    public GameObject winScreen;

    private GameManager gameManager;
    private LevelManager levelManager;
    
    [SerializeField] private TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        levelManager = FindObjectOfType<LevelManager>();

        levelText.text = "Level " + levelManager.levelIndex;
    }
    
    public void Pause()
    {
        pauseScreen.SetActive(true);
        gameManager.isGameActive = false;
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pauseScreen.SetActive(false);
        StartCoroutine("SetGameActive");
        Time.timeScale = 1;
    }

    IEnumerator SetGameActive()
    {
        yield return new WaitForSeconds(0.5f);
        gameManager.isGameActive = true;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        levelManager.RestartScene();
    }

    public void NextLevel()
    {
        Time.timeScale = 1;
        levelManager.LoadNextLevel();
    }
}
