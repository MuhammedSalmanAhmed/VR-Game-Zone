using UnityEngine;

public class LevelCompleteManager : MonoBehaviour
{
    public GameObject levelCompletePanel; // Reference to the Level Complete UI
    public GameObject[] teleportationAnchors; // Teleportation anchors to activate
    public Transform head;
    public float spawnDistance = 2f;

    private void Start()
    {
        levelCompletePanel.SetActive(false);
        foreach (var anchor in teleportationAnchors)
        {
            anchor.SetActive(false);
        }
    }

    public void ShowLevelCompleteUI()
    {
        PositionLevelCompletePanel();
        levelCompletePanel.SetActive(true);

        Invoke(nameof(HideLevelCompleteUI), 3f);
    }

    private void PositionLevelCompletePanel()
    {
        if (head != null && levelCompletePanel != null)
        {
            Vector3 forwardDirection = new Vector3(head.forward.x, 0, head.forward.z).normalized;
            levelCompletePanel.transform.position = head.position + forwardDirection * spawnDistance;
            levelCompletePanel.transform.LookAt(new Vector3(head.position.x, levelCompletePanel.transform.position.y, head.position.z));
            levelCompletePanel.transform.forward *= -1;
        }
    }

    private void HideLevelCompleteUI()
    {
        levelCompletePanel.SetActive(false);

        foreach (var anchor in teleportationAnchors)
        {
            anchor.SetActive(true);
        }
    }
}
