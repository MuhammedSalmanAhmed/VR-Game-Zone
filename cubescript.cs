using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import the TextMeshPro namespace

public class CubeSizeController : MonoBehaviour
{
    [Header("UI Components")]
    public Slider sizeSlider;   // Reference to the UI slider
    public TMP_Text sizeLabel;  // TextMeshPro reference for size display

    [Header("Cube Settings")]
    public GameObject cube;     // Reference to the cube object
    public float minSize = 0.5f; // Minimum scale
    public float maxSize = 5f;   // Maximum scale

    private void Start()
    {
        // Validate references
        if (sizeSlider == null || cube == null)
        {
            Debug.LogError("Please assign the Slider and Cube references in the Inspector.");
            return;
        }

        // Initialize slider properties
        sizeSlider.minValue = minSize;
        sizeSlider.maxValue = maxSize;
        sizeSlider.value = cube.transform.localScale.x; // Sync slider with cube's initial size

        // Update the size label with the initial value
        UpdateSizeLabel(sizeSlider.value);

        // Add listener to the slider
        sizeSlider.onValueChanged.AddListener(UpdateCubeSize);
    }

    private void UpdateCubeSize(float newSize)
    {
        // Update the cube's size
        if (cube != null)
        {
            cube.transform.localScale = new Vector3(newSize, newSize, newSize);
        }

        // Update size text label
        UpdateSizeLabel(newSize);
    }

    private void UpdateSizeLabel(float size)
    {
        if (sizeLabel != null)
        {
            sizeLabel.text = $"Size: {size:F2}"; // Display size with two decimal places
        }
    }

    private void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        if (sizeSlider != null)
        {
            sizeSlider.onValueChanged.RemoveListener(UpdateCubeSize);
        }
    }
}
