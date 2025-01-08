using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class collide : MonoBehaviour
{
    public float collisionForce = 10f; // Force to apply when colliding
    public float moveSpeed = 5f; // Speed of movement to the right after collision
    private bool isColliding = false;


    void Start()
    {
    
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Reload the current scene when the collision happens
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
