using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsmove : MonoBehaviour
{
    public float moveSpeed = 100f; // Speed at which the obstacle moves towards the player
    public float destroyZPosition = -10f; // Position at which the obstacle is destroyed
    public float startDelay = 5f; // Delay before the obstacle starts moving
    private bool canMove = false; // Flag to track when the obstacle can move

    void Start()
    {
        // Start the delay before enabling movement
        StartCoroutine(StartMovingAfterDelay());
    }

    IEnumerator StartMovingAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(startDelay);
        canMove = true; // Enable movement
    }

    void Update()
    {
        if (canMove)
        {
            // Move the obstacle towards the player
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

            // Destroy the obstacle if it goes past the player
            if (transform.position.z < destroyZPosition)
            {
                Destroy(gameObject);
            }
        }
    }
}
