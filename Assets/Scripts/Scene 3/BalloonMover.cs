using UnityEngine;

public class BalloonMover : MonoBehaviour
{
    public Transform targetPoint;
    private float speed = 1f; // Default speed
    private bool isBroken = false;
    private bool hasAttacked = false;
    private Break_Ghost breakGhost;
    private GameOverUIManager gameOverUIManager;

    void Start()
    {
        if (targetPoint == null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                targetPoint = mainCamera.transform;
            }
            else
            {
                Debug.LogError("Main Camera not found! Ensure your XR Origin has a Main Camera tag.");
            }
        }

        breakGhost = GetComponent<Break_Ghost>();
        gameOverUIManager = FindObjectOfType<GameOverUIManager>();

        if (breakGhost == null)
        {
            Debug.LogError("Break_Ghost component is not attached to the balloon object!");
        }

        if (gameOverUIManager == null)
        {
            Debug.LogError("GameOverUIManager not found in the scene!");
        }
    }

    void Update()
    {
        if (isBroken || hasAttacked) return;

        if (targetPoint != null)
        {
            Vector3 targetPosition = targetPoint.position;
            targetPosition.y = transform.position.y;

            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            transform.position += direction * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPoint.position) < 0.5f)
            {
                AttackTarget();
            }
        }
        else
        {
            Debug.LogWarning("Target point is not assigned for " + gameObject.name);
        }
    }

    private void AttackTarget()
    {
        hasAttacked = true;

        if (breakGhost != null)
        {
            breakGhost.play_anim();
            StartCoroutine(WaitForAttackAnimation());
        }
        else
        {
            Debug.LogWarning("Break_Ghost component not found on " + gameObject.name);
        }
    }

    private System.Collections.IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(breakGhost.ghost.GetCurrentAnimatorStateInfo(0).length);

        if (!isBroken && gameOverUIManager != null)
        {
            gameOverUIManager.ShowGameOverUI();
        }
    }

    public void StopMovementOnBreak()
    {
        isBroken = true;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
