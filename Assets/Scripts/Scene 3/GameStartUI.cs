using System.Collections;
using UnityEngine;

public class GameStartUI : MonoBehaviour
{
    public GameObject uiPanel; // The UI panel to display
    public GameObject balloonSpawner; // Reference to the balloon spawner
    public GameObject ballSpawner; // Reference to the ball spawner
    public float displayDuration = 5f; // Duration to show the UI

    void Start()
    {
        // Disable game-related systems at the start
        if (balloonSpawner != null) balloonSpawner.SetActive(false);
        if (ballSpawner != null) ballSpawner.SetActive(false);

        // Show the UI panel
        if (uiPanel != null) uiPanel.SetActive(true);

        // Start the coroutine to hide the UI and start the game
        StartCoroutine(StartGameAfterDelay());
    }

    private IEnumerator StartGameAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the UI panel
        if (uiPanel != null) uiPanel.SetActive(false);

        // Enable game-related systems
        if (balloonSpawner != null) balloonSpawner.SetActive(true);
        if (ballSpawner != null) ballSpawner.SetActive(true);
    }
}
