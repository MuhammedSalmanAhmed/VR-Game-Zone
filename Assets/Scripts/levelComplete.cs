using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelComplete : MonoBehaviour
{
   public void LoadNext()
{
    Debug.Log("Button Clicked! Attempting to load next scene...");
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

    if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
    else
    {
        Debug.Log("No more levels!");
    }
}

}
