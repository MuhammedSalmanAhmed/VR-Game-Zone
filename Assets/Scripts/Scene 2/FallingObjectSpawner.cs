using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject[] objectsToSpawn; // Array of objects to spawn
    public float spawnInterval = 3f; // Time interval between spawns
    public Transform spawnLineStart; // Start point of the spawn line
    public Transform spawnLineEnd; // End point of the spawn line

    [Header("Object Settings")]
    public float objectFallSpeed = 5f; // Speed at which the objects fall

    private float spawnTimer;

    void Start()
    {
        // Start the spawn timer
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

        // Randomly select a spawn position along the line
        float spawnPositionX = Random.Range(spawnLineStart.position.x, spawnLineEnd.position.x);
        Vector3 spawnPosition = new Vector3(spawnPositionX, spawnLineStart.position.y, spawnLineStart.position.z);

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
    }

    private void OnDrawGizmos()
    {
        // Draw the spawn line in the Scene view
        if (spawnLineStart != null && spawnLineEnd != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(spawnLineStart.position, spawnLineEnd.position);
        }
    }
}
