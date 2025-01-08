using UnityEngine;

public class BalloonBreakerOnCollision : MonoBehaviour
{
    public GameObject ghost_normal; // Normal balloon
    public GameObject ghost_Parts;  // Broken balloon parts
    public bool Is_Breaked = false; // Track if the balloon is already broken
    public float breakSpeedThreshold = 0.003f; // Minimum speed required to break the balloon

    private GameOverUIManager gameOverUIManager;

    void Start()
    {
        ghost_normal.SetActive(true);
        ghost_Parts.SetActive(false);

        gameOverUIManager = FindObjectOfType<GameOverUIManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Is_Breaked) return;

        BallCollisionHandler ballHandler = collision.gameObject.GetComponent<BallCollisionHandler>();
        if (ballHandler != null)
        {
            float ballSpeed = ballHandler.GetCurrentSpeed();
            if (ballSpeed >= breakSpeedThreshold)
            {
                BreakBalloon();
            }
            else
            {
                Debug.Log($"Ball speed {ballSpeed} is too low to break the balloon.");
            }
        }
    }

    public void BreakBalloon()
    {
        Is_Breaked = true;

        GetComponent<BalloonMover>()?.StopMovementOnBreak();
        ghost_normal.SetActive(false);
        ghost_Parts.SetActive(true);

        // Unregister the balloon from GameOverUIManager
        gameOverUIManager?.UnregisterBalloon(gameObject);

        Debug.Log("Balloon broken due to high-speed collision.");
        Destroy(gameObject, 3f);

        BalloonSpawner spawner = FindObjectOfType<BalloonSpawner>();
        if (spawner != null)
        {
            spawner.RemoveBalloon(gameObject);
        }
    }
}
