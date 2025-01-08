using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyDisappear : MonoBehaviour
{

    public float lifetime = 5f; // Time in seconds before the galaxy disappears

    void Start()
    {
        // Destroy the GameObject after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}

