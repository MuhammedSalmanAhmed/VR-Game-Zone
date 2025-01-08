using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collide : MonoBehaviour
{
    public float collisionForce = 10f; // Force to apply when colliding
    public float moveSpeed = 5f; // Speed of movement to the right after collision
    private bool isColliding = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Ensure it's not affected by gravity or other forces
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Apply force when the collision happens
            rb.isKinematic = false; // Make Rigidbody responsive to forces
            Vector3 forceDirection = -collision.contacts[0].normal; // Apply force opposite to the collision surface
            rb.AddForce(forceDirection * collisionForce, ForceMode.Impulse);

            // Start moving to the right after the collision
            isColliding = true;
        }
    }

    void Update()
    {
        if (isColliding)
        {
            // Move the XR Origin to the right
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }
}
