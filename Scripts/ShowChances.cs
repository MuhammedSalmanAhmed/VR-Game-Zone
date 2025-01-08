using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowChances : MonoBehaviour
{
    public Text chancesText;     // Reference to the UI Text for displaying chances
    private GameManager gameManager;  // Reference to the GameManager

    void Start()
    {
        // Try finding the GameManager and ensure it exists
        gameManager = FindObjectOfType<GameManager>();
        
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
            return;
        }

        // Initialize UI with current chances
        UpdateChancesUI();
    }

    void Update()
    {
        // Update UI if chances change during the game
        if (gameManager != null)
        {
            UpdateChancesUI();
        }
    }

    public void UpdateChancesUI()
    {
        // Ensure the gameManager reference is not null
        if (gameManager != null)
        {
            chancesText.text = "Chances: " + gameManager.GetChances().ToString();
        }
        else
        {
            Debug.LogError("GameManager reference is null in UpdateChancesUI");
        }
    }
}
