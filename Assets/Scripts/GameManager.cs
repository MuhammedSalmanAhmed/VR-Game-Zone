using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public float startDelay = 5f; // Delay before the obstacle starts moving
    public GameObject completeLevelUI;
    public GameObject startLevelUI;
    public GameObject gameoverUI;
    public GameObject button;
    public GameObject Teleport;

    // Private chances variable
    private int chances = 3;

    // Getter method to access chances
    public int GetChances()
    {
        return chances;
    }
 public void ReduceChances()
    {
        chances--; // Reduce chances
        if (chances <= 0)
        {
            GameOver();
        }
    }


    private void GameOver()
    {
        gameHasEnded = true;
        Debug.Log("GAME OVER");
        gameoverUI.SetActive(true);
        completeLevelUI.SetActive(false);
        Invoke("Restart", restartDelay);
    }

    // Method to complete the level (optional)
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
StartCoroutine(StartMovingAfterDelay());
    }

    // Method to start the level (optional)
    public void startLevel()
    {
        startLevelUI.SetActive(false);
        
    }

 IEnumerator StartMovingAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(startDelay);
        completeLevelUI.SetActive(false);
        button.SetActive(false);
        Teleport.SetActive(true);
    }
    // Restart the level
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reload the current scene
    }

}
