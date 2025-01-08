using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameOverUIManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the Game Over panel
    public GameObject gameUIPanel;   // Reference to the Game UI panel
    public List<GameObject> balloons = new List<GameObject>(); // List of active balloons
    public GameObject ball;         // Reference to the player's ball

    [Header("UI Placement Settings")]
    public Transform head;          // Reference to the player's head transform (e.g., XR camera or main camera)
    public float spawnDistance = 2f; // Distance at which the Game Over panel appears in front of the player

    void Start()
    {
        // Hide the Game Over panel at the start
        gameOverPanel.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        // Hide game UI and stop balloons/ball
        gameUIPanel?.SetActive(false);

        // Stop balloon spawning
        BalloonSpawner spawner = FindObjectOfType<BalloonSpawner>();
        if (spawner != null)
        {
            spawner.StopSpawning();
        }

        // Destroy all balloons and the ball
        foreach (GameObject balloon in balloons)
        {
            if (balloon != null)
                Destroy(balloon);
        }
        balloons.Clear();

        if (ball != null)
        {
            Destroy(ball);
        }

        // Position the Game Over panel in front of the player
        PositionGameOverPanel();

        // Show the Game Over panel
        gameOverPanel.SetActive(true);
    }

    private void PositionGameOverPanel()
    {
        // Place the panel in front of the player's head
        if (head != null && gameOverPanel != null)
        {
            Vector3 forwardDirection = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            gameOverPanel.transform.position = head.position + forwardDirection * spawnDistance;

            // Make the panel face the player
            gameOverPanel.transform.LookAt(new Vector3(head.position.x, gameOverPanel.transform.position.y, head.position.z));
            gameOverPanel.transform.forward *= -1; // Reverse forward direction to face the player
        }
        else
        {
            Debug.LogWarning("Head or Game Over Panel reference is missing!");
        }
    }

    public void RegisterBalloon(GameObject balloon)
    {
        if (!balloons.Contains(balloon))
        {
            balloons.Add(balloon);
        }
    }

    public void UnregisterBalloon(GameObject balloon)
    {
        if (balloons.Contains(balloon))
        {
            balloons.Remove(balloon);
        }
    }

    public void RegisterBall(GameObject newBall)
    {
        ball = newBall;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    public void ExitToStartScene()
    {
        SceneManager.LoadScene("Start Scene"); // Replace "StartScene" with your start menu scene name
    }
}
