using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportToScene : MonoBehaviour
{
    [Tooltip("Build index of the scene to load when this anchor is activated.")]
    public int targetSceneIndex;

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
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.LogError($"Invalid build index {targetSceneIndex}. Ensure it's within the valid range (0 to {SceneManager.sceneCountInBuildSettings - 1}).");
        }
    }
}