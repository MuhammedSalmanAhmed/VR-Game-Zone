using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LockInteraction : MonoBehaviour
{
    public GameObject door; // Reference to the door
    public Material lockedMaterial; // Material for the locked state
    public Material unlockedMaterial; // Material for the unlocked state

    private XRSocketInteractor socketInteractor;
    private Renderer doorRenderer;

    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        doorRenderer = door.GetComponent<Renderer>();

        // Ensure the door starts in a "locked" state
        ChangeDoorMaterial(lockedMaterial);

        // Subscribe to the events
        socketInteractor.selectEntered.AddListener(OnKeyInserted);
        socketInteractor.selectExited.AddListener(OnKeyRemoved);
    }

    private void OnKeyInserted(SelectEnterEventArgs args)
    {
        // Key inserted: Unlock the door
        ChangeDoorMaterial(unlockedMaterial);
        Debug.Log("Key inserted: Door unlocked.");
    }

    private void OnKeyRemoved(SelectExitEventArgs args)
    {
        // Key removed: Lock the door
        ChangeDoorMaterial(lockedMaterial);
        Debug.Log("Key removed: Door locked.");
    }

    private void ChangeDoorMaterial(Material material)
    {
        if (doorRenderer != null && material != null)
        {
            doorRenderer.material = material;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from events to avoid memory leaks
        socketInteractor.selectEntered.RemoveListener(OnKeyInserted);
        socketInteractor.selectExited.RemoveListener(OnKeyRemoved);
    }
}
