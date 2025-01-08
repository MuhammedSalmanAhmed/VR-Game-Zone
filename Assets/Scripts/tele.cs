using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class tele : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Build index of the scene to load when this anchor is activated.")]
    public int targetSceneIndex;

    [Tooltip("Whether to load the scene asynchronously.")]
    public bool loadAsync = true;

    private XRBaseInteractable interactable;

    void Awake()
    {
        // Get the XR interactable component on the GameObject
        interactable = GetComponent<XRBaseInteractable>();

        if (interactable == null)
        {
            Debug.LogError("XRBaseInteractable component is missing. Please add one to this GameObject.");
        }
    }

    void OnEnable()
    {
        // Subscribe to the select event
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnTeleportAnchorActivated);
        }
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnTeleportAnchorActivated);
        }
    }

    private void OnTeleportAnchorActivated(SelectEnterEventArgs args)
    {
        // Check if the target scene index is valid
        if (targetSceneIndex >= 0 && targetSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (loadAsync)
            {
                StartCoroutine(LoadSceneAsync(targetSceneIndex));
            }
            else
            {
                SceneManager.LoadScene(targetSceneIndex);
            }
        }
        else
        {
            Debug.LogError($"Invalid build index {targetSceneIndex}. Ensure it's within the valid range (0 to {SceneManager.sceneCountInBuildSettings - 1}).");
        }
    }

    private System.Collections.IEnumerator LoadSceneAsync(int sceneIndex)
    {
        // Start loading the scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = true;

        while (!asyncOperation.isDone)
        {
            Debug.Log($"Loading Scene: {sceneIndex} | Progress: {asyncOperation.progress * 100:F2}%");
            yield return null; // Wait until the next frame
        }
    }

    private void OnValidate()
    {
        // Ensure the target scene index is valid during edit mode
        if (targetSceneIndex < 0 || targetSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning($"Target scene index {targetSceneIndex} is out of range. Update it to a valid index.");
        }
    }
}
