using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class FallingObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public float spawnInterval = 3f; // Time interval between spawns
    public Transform spawnLineStart; // Start point of the spawn line
    public Transform spawnLineEnd; // End point of the spawn line

    [Header("Object Settings")]
    public float objectFallSpeed = 5f; // Speed at which the objects fall

    [Header("Collision Settings")]
    public GameObject floor; // The floor object to detect collisions
    public GameObject player; // The player object (XR Origin)

    private float spawnTimer;

    void Start()
    {
        // Initialize the spawn timer
        spawnTimer = spawnInterval;
    }

    void Update()
    {
        // Update the spawn timer
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0f)
        {
            SpawnObject();
            spawnTimer = spawnInterval; // Reset the timer
        }
    }

    void SpawnObject()
    {
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No objects assigned to spawn!");
            return;
        }

        // Randomly select an object to spawn
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Select a spawn position strictly along the spawn line
        float t = Random.Range(0f, 1f); // Interpolation factor between 0 (start) and 1 (end)
        Vector3 spawnPosition = Vector3.Lerp(spawnLineStart.position, spawnLineEnd.position, t);

        // Instantiate the object at the spawn position
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Add downward movement to the spawned object
        Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = spawnedObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false; // Disable default gravity
        rb.velocity = Vector3.down * objectFallSpeed; // Add custom downward velocity

        // Add a CollisionHandler script to handle destruction and restart logic
        CollisionHandler collisionHandler = spawnedObject.AddComponent<CollisionHandler>();
        collisionHandler.floor = floor; // Assign the floor object
        collisionHandler.player = player; // Assign the player object
    }

    private void OnDrawGizmos()
    {
        // Draw the spawn line in the Scene view for visualization
        if (spawnLineStart != null && spawnLineEnd != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(spawnLineStart.position, spawnLineEnd.position);
        }
    }
}

public class CollisionHandler : MonoBehaviour
{
    public GameObject floor; // The floor object
    public GameObject player; // The player object (XR Origin)

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collides with the assigned floor
        if (collision.gameObject == floor)
        {
            Destroy(gameObject); // Destroy the object
        }

        // Check if the object collides with the player
        if (collision.gameObject == player)
        {
            // Restart the current level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
