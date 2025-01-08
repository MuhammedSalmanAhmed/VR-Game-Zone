using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BallSpawnerWithRespawn : MonoBehaviour
{
    public GameObject ballPrefab; // Prefab of the ball to spawn
    public Transform head; // Transform of the VR headset (head)
    public float spawnDistance = 2f; // Distance to spawn the ball in front of the player
    public AudioClip destroySound; // The sound to play on destruction

    private GameOverUIManager gameOverUIManager;
    private GameObject currentBall;

    void Start()
    {
        gameOverUIManager = FindObjectOfType<GameOverUIManager>();
        // Spawn the ball at the start of the game
        SpawnBall();
    }

    private void SpawnBall()
    {
        // Instantiate a new ball at the spawn point in front of the head
        if (ballPrefab != null && head != null)
        {
            Vector3 spawnPosition = head.position + (new Vector3(head.forward.x, -0.2f, head.forward.z).normalized * spawnDistance);
            currentBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

            // Register ball with GameOverUIManager
            gameOverUIManager?.RegisterBall(currentBall);

            // Add collision handling to the spawned ball
            BallCollisionHandler collisionHandler = currentBall.AddComponent<BallCollisionHandler>();
            collisionHandler.destroySound = destroySound;
            collisionHandler.onBallDestroyed = SpawnBall; // Callback to respawn the ball

            // Ensure the ball is initially kinematic
            Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                ballRigidbody.isKinematic = true;
            }

            // Add grab interaction handling
            XRGrabInteractable grabInteractable = currentBall.GetComponent<XRGrabInteractable>();
            if (grabInteractable == null)
            {
                grabInteractable = currentBall.AddComponent<XRGrabInteractable>();
            }

            // Add event listeners for grabbing and releasing the ball
            grabInteractable.selectEntered.AddListener(OnBallGrabbed);
            grabInteractable.selectExited.AddListener(OnBallReleased);
        }
        else
        {
            Debug.LogError("BallPrefab or Head transform is not assigned.");
        }
    }

    private void OnBallGrabbed(SelectEnterEventArgs args)
    {
        // Enable physics (disable isKinematic) when the ball is grabbed
        Rigidbody ballRigidbody = args.interactableObject.transform.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = false;
        }
    }

    private void OnBallReleased(SelectExitEventArgs args)
    {
        // Ensure isKinematic is off when the ball is released
        Rigidbody ballRigidbody = args.interactableObject.transform.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = false;
        }
    }
}

public class BallCollisionHandler : MonoBehaviour
{
    public AudioClip destroySound; // The sound to play on destruction
    public System.Action onBallDestroyed; // Callback to respawn the ball
    private Rigidbody ballRigidbody;

    void Start()
    {
        ballRigidbody = GetComponent<Rigidbody>();
        if (ballRigidbody == null)
        {
            Debug.LogError("Rigidbody is missing on the ball.");
        }
    }

    public float GetCurrentSpeed()
    {
        if (ballRigidbody != null)
        {
            return ballRigidbody.velocity.magnitude; // Return the current speed of the ball
        }
        return 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the ball collides with a balloon
        if (collision.gameObject.CompareTag("Balloon"))
        {
            // Get the speed threshold from the balloon's script
            var balloonBreaker = collision.gameObject.GetComponent<BalloonBreakerOnCollision>();
            if (balloonBreaker != null)
            {
                float ballSpeed = GetCurrentSpeed();
                if (ballSpeed >= balloonBreaker.breakSpeedThreshold)
                {
                    Debug.Log($"Ball speed {ballSpeed} is high enough to break the balloon.");
                    balloonBreaker.BreakBalloon();
                    Destroy(gameObject); // Destroy the ball after breaking the balloon

                    // Play destruction sound
                    if (destroySound != null)
                    {
                        PlaySound();
                    }

                    // Trigger the respawn callback
                    onBallDestroyed?.Invoke();
                }
                else
                {
                    Debug.Log($"Ball speed {ballSpeed} is too low to break the balloon. Ball will remain active.");
                    // Allow the ball to remain active and fall
                }
            }
        }
    }

    private void PlaySound()
    {
        GameObject tempAudioSource = new GameObject("TempAudio");
        AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();

        tempSource.clip = destroySound;
        tempSource.Play();

        Destroy(tempAudioSource, destroySound.length);
    }
}
