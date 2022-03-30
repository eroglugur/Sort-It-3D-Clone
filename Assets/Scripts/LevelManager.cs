using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int levelIndex = 1;
    
    [SerializeField] private GameObject[] levels = new GameObject[4];
    
    public GameObject Level()
    {
        levels[levelIndex - 1] = Resources.Load<GameObject>("LevelPrefabs/" + "Level " + PlayerPrefs.GetInt("LevelIndex"));

        return levels[PlayerPrefs.GetInt("LevelIndex") - 1];
    }
    
    private void Start()
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
        PlayerPrefs.SetInt("LevelIndex", levelIndex++);
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
