using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameManager gameManager;

	void OnTriggerEnter()
{
    Debug.Log("Trigger Entered");  // Check if trigger is activated
    gameManager.CompleteLevel();
    Invoke("LoadNextLevel", 5f);
}

void LoadNextLevel()
{
    Debug.Log("Loading Next Level"); // Check if LoadNextLevel is called
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        Debug.Log("Loading Scene Index: " + nextSceneIndex);
        SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
        Debug.LogWarning("No more levels to load. Scene index out of range.");
    }
}


}
