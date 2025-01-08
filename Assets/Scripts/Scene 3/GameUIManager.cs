using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public TextMeshProUGUI enemiesDefeatedText;
    public TextMeshProUGUI enemiesRemainingText;
    public GameObject gameUIPanel; // Reference to the Game UI panel (set in Inspector)

    private int enemiesDefeated = 0;

    void Start()
    {
        // Initially hide the UI
        gameUIPanel.SetActive(false);

        // Show the UI after 5 seconds
        Invoke(nameof(ShowGameUI), 5f);
    }

    void OnEnable()
    {
        BalloonSpawner.OnBalloonCountUpdated += UpdateUI;
    }

    void OnDisable()
    {
        BalloonSpawner.OnBalloonCountUpdated -= UpdateUI;
    }

    private void ShowGameUI()
    {
        gameUIPanel.SetActive(true);
    }

    private void UpdateUI(int totalSpawned, int activeCount)
    {
        enemiesDefeated = totalSpawned - activeCount;
        enemiesDefeatedText.text = $"Score: {enemiesDefeated}";
        enemiesRemainingText.text = $"Enemies Present: {activeCount}";
    }
}
