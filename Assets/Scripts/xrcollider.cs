using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xrcollider : MonoBehaviour

{
    public AudioClip collisionSound; // Sound to play on collision
    public ParticleSystem collisionEffect; // Particle effect on collision
    public Camera mainCamera; // Reference to the VR camera for screen shake
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource for sound playback
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = collisionSound;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is tagged as "Obstacle"
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Collision with obstacle detected!");

            // Play collision sound
            if (collisionSound != null)
                audioSource.Play();

            // Trigger particle effect
            if (collisionEffect != null)
                Instantiate(collisionEffect, other.transform.position, Quaternion.identity);

            // Screen shake
            if (mainCamera != null)
                StartCoroutine(ScreenShake());
        }
    }

    IEnumerator ScreenShake()
    {
        float duration = 0.2f; // Duration of the shake
        float magnitude = 0.1f; // Intensity of the shake

        Vector3 originalPosition = mainCamera.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCamera.transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = originalPosition; // Reset position
    }
}


