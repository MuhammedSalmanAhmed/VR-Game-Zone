using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of the movement
    public float moveRange = 5f;  // How far the obstacle moves to the left and right
    private Vector3 startPosition;  // Initial position of the obstacle

    void Start()
    {
        // Store the initial position of the obstacle
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the obstacle back and forth on the X-axis
        float movement = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        transform.position = new Vector3(startPosition.x + movement, transform.position.y, transform.position.z);
    }
}
