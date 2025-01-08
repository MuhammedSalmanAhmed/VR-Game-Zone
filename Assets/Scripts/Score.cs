using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;    // Reference to the UI Text object
    private float score;      // Variable to store the score
    public float scoreRate = 10f; // Rate at which the score increases (points per second)
    private float timer;      // Timer to track the elapsed time
    private bool isScoring;   // Flag to check if the scoring is active

    void Start()
    {
        // Initialize score to 0
        score = 0f;
        timer = 0f;
        isScoring = false;
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Start scoring after 5 seconds
        if (timer >= 5f && !isScoring)
        {
            isScoring = true;
        }

        // Stop scoring after 10 seconds (5 seconds to start + 5 seconds to run)
        if (timer >= 14f)
        {
            isScoring = false;
        }

        // Increase the score if scoring is active
        if (isScoring)
        {
            score += scoreRate * Time.deltaTime;
        }

        // Update the score text with "Score: " format
        scoreText.text = "Score: " + score.ToString("0");
    }
}
