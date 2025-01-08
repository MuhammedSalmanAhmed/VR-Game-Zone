using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardforce = 1000f;
    public float Sideforce = 1000f;
    private bool canMove = false; // Flag to check if movement is allowed
    private bool hasFallen = false; // Flag to track if player has already fallen
    public float fallThreshold = -1f; // Y position threshold for falling

    void Start()
    {
        // Start the coroutine to wait for 5 seconds
        StartCoroutine(StartMovementAfterDelay(5f));
    }

    // Coroutine to wait for a specific delay
    IEnumerator StartMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 'delay' seconds
        canMove = true; // Allow movement after the delay
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!canMove)
            return; // Don't move until the delay is over

        // Apply forward force
        rb.AddForce(0, 0, forwardforce * Time.deltaTime);

        // Move right
        if (Input.GetKey("d"))
        {
            rb.AddForce(Sideforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        // Move left
        if (Input.GetKey("a"))
        {
            rb.AddForce(-Sideforce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        // If the player falls below the fall threshold and hasn't already been processed
        if (rb.position.y < fallThreshold && !hasFallen)
        {
            FindObjectOfType<GameManager>().ReduceChances(); // Reduce chances
            hasFallen = true; // Mark that the player has fallen
            StopPlayer(); // Stop player's movement or reset player position
        }
        else if (rb.position.y > fallThreshold && hasFallen) // Reset when player is back above the fall threshold
        {
            hasFallen = false; // Allow fall detection again
        }
    }

    // Stop the player's movement after falling
    private void StopPlayer()
    {
        // Optionally, you can reset the player's position or stop the velocity
        rb.velocity = Vector3.zero; // Stop the player's movement
        rb.position = new Vector3(0, 1, 0); // Reset the player's position (optional)
    }
}
