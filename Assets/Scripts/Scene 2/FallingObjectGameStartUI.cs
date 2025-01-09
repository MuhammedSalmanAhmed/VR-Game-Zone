using System.Collections;
using UnityEngine;

public class FallingObjectGameStartUI : MonoBehaviour
{
    public GameObject uiPanel; // The UI panel to display
    public GameObject fallingObjectSpawner; // Reference to the falling object spawner
    public float displayDuration = 5f; // Duration to show the UI

    void Start()
    {
        // Disable the falling object spawner at the start
        if (fallingObjectSpawner != null) fallingObjectSpawner.SetActive(false);

        // Show the UI panel
        if (uiPanel != null) uiPanel.SetActive(true);

        // Start the coroutine to hide the UI and start spawning falling objects
        StartCoroutine(StartSpawningAfterDelay());
    }

    private IEnumerator StartSpawningAfterDelay()
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(displayDuration);

        // Hide the UI panel
        if (uiPanel != null) uiPanel.SetActive(false);

        // Enable the falling object spawner
        if (fallingObjectSpawner != null) fallingObjectSpawner.SetActive(true);
    }
}
