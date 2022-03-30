using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelIndex = PlayerPrefs.GetInt("LevelIndex");

    [SerializeField] private GameObject[] levels = new GameObject[4];

    private GameObject Level()
    {
        if (PlayerPrefs.GetInt("LevelIndex") > 4)
        {
            PlayerPrefs.SetInt("LevelIndex", 1);
        }
        levelIndex = PlayerPrefs.GetInt("LevelIndex");

        levels[PlayerPrefs.GetInt("LevelIndex") - 1] =
            Resources.Load<GameObject>("LevelPrefabs/" + "Level " + PlayerPrefs.GetInt("LevelIndex"));

        return levels[PlayerPrefs.GetInt("LevelIndex") - 1];
    }


    private void Awake()
    {
        Instantiate(Level());
    }

    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("LevelIndex", levelIndex);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}