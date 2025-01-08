using UnityEngine;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public GameObject startButtonUI; // Reference to the Start Button UI
    public GameObject teleportCanvas; // Reference to the canvas showing "Teleport to any game"
    public GameObject[] teleportAnchors; // Array of teleportation anchors

    private void Start()
    {
        // Ensure initial state
        teleportCanvas.SetActive(false);
        foreach (GameObject anchor in teleportAnchors)
        {
            anchor.SetActive(false);
        }
    }

    public void OnPlayButtonClicked()
    {
        // Hide the Start Button UI
        startButtonUI.SetActive(false);

        // Show the "Teleport to any game" canvas
        teleportCanvas.SetActive(true);

        // Start coroutine to activate teleport anchors after a delay
        StartCoroutine(ActivateTeleportAnchors());
    }

    private System.Collections.IEnumerator ActivateTeleportAnchors()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        teleportCanvas.SetActive(false); // Hide the teleport canvas
        foreach (GameObject anchor in teleportAnchors)
        {
            anchor.SetActive(true); // Activate teleportation anchors
        }
    }

    public void OnExitButtonClicked()
    {
        Application.Quit(); // Exit the application
        Debug.Log("Game Exit Triggered.");
    }
}
