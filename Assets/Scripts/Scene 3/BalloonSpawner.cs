using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour
{
    [Header("Balloon Settings")]
    public GameObject[] balloonPrefabs; // Array to hold the different balloon types
    public int balloonsPerLevel = 50; // Total number of balloons for the level
    public float spawnInterval = 3f; // Time between spawns in seconds
    public float balloonSpeed = 1f; // Speed of balloons for this level

    [Header("Spawn Area Settings")]
    public Transform target; // Center point (e.g., player or XR Origin)
    public float spawnRadius = 15f; // Radius in which balloons can spawn
    public float exclusionRadius = 10f; // Radius within which balloons cannot spawn

    private int balloonsSpawned = 0; // Track the number of spawned balloons
    private List<GameObject> activeBalloons = new List<GameObject>(); // Track active balloons

    public delegate void BalloonCountUpdate(int totalSpawned, int activeCount);
    public static event BalloonCountUpdate OnBalloonCountUpdated;

    private GameOverUIManager gameOverUIManager;

    private bool isGameOver = false; // Flag to indicate game over state
    private bool isLevelComplete = false;
    public GameObject levelCompleteManager; // Reference to LevelCompleteManager

    void Start()
    {
        // Reference the GameOverUIManager
        gameOverUIManager = FindObjectOfType<GameOverUIManager>();

        // Start spawning balloons
        InvokeRepeating(nameof(SpawnBalloon), 0f, spawnInterval);
    }

    private void SpawnBalloon()
    {
        if (balloonsSpawned >= balloonsPerLevel)
        {
            CancelInvoke(nameof(SpawnBalloon));
            Debug.Log("All balloons for this level have been spawned!");
            return;
        }

        GameObject balloonPrefab = balloonPrefabs[Random.Range(0, balloonPrefabs.Length)];
        if (balloonPrefab == null)
        {
            Debug.LogError("Balloon prefab is missing!");
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newBalloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
        activeBalloons.Add(newBalloon);

        // Set the speed of the balloon
        BalloonMover balloonMover = newBalloon.GetComponent<BalloonMover>();
        if (balloonMover != null)
        {
            balloonMover.SetSpeed(balloonSpeed);
        }

        // Register balloon with the GameOverUIManager
        gameOverUIManager?.RegisterBalloon(newBalloon);

        balloonsSpawned++;
        UpdateBalloonCount();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition;
        do
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float distance = Random.Range(exclusionRadius, spawnRadius);
            randomPosition = new Vector3(
                Mathf.Cos(angle) * distance,
                0f,
                Mathf.Sin(angle) * distance
            ) + target.position;
        }
        while (Vector3.Distance(randomPosition, target.position) < exclusionRadius);

        return randomPosition;
    }

    public void RemoveBalloon(GameObject balloon)
    {
        if (activeBalloons.Contains(balloon))
        {
            activeBalloons.Remove(balloon);
            UpdateBalloonCount();

            if (activeBalloons.Count == 0)
            {
                OnLevelComplete();
            }
        }
    }

    private void UpdateBalloonCount()
    {
        OnBalloonCountUpdated?.Invoke(balloonsSpawned, activeBalloons.Count);
    }

    public void StopSpawning()
    {
        isGameOver = true; // Set game over state
        CancelInvoke(nameof(SpawnBalloon)); // Stop spawning balloons
    }

    private void OnLevelComplete()
    {
        isLevelComplete = true;
        CancelInvoke(nameof(SpawnBalloon));
        levelCompleteManager?.GetComponent<LevelCompleteManager>()?.ShowLevelCompleteUI();
    }
}
