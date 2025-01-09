using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    // Method to exit the game
    public void ExitGame()
    {
        Debug.Log("Exiting the game...");
        Application.Quit(); // Quits the application
    }

    // Method to load the Start Scene
    public void LoadStartScene()
    {
        Debug.Log("Loading Start Scene...");
        SceneManager.LoadScene("Start Scene"); // Replace with the exact name of your Start Scene
    }
}
