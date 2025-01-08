using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 3.0f; // Base speed
    public float boostMultiplier = 2.0f; // Boost multiplier for faster movement
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input from the XR device (e.g., joystick or thumbstick)
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // Calculate movement direction and scale it by speed
        Vector3 moveDirection = new Vector3(input.x, 0, input.y).normalized * moveSpeed;

        // Apply movement
        characterController.Move(moveDirection * Time.deltaTime);

        // Optional: Increase speed with a "boost" button
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Boost"))
        {
            moveDirection *= boostMultiplier;
        }
    }
}
