using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nest : MonoBehaviour
{
    public void LoadNext()
    {
        // Get the current scene's build index and load the next one
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene index is within the build settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // Load the next level
        }
        else
        {
            Debug.Log("No more levels!"); // Optional: Handle the case where there are no more levels
        }
    }
}
