using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int levelIndex = 1;

    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }
    }
    
   public void RestartScene()
   {
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex);
    }

   public void GetWinScreen()
   {
       Debug.Log("WIN!");
   }
   
}
