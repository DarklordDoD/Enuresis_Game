using UnityEngine;
using UnityEngine.UI;

public class CloseParent : MonoBehaviour
{
    // Reference to the parent GameObject
    public GameObject parentObject;

    // Reference to the button component
    private Button button;

    void Start()
    {
        // Get the button component
        button = GetComponent<Button>();

        // Add a listener to the button click event
        button.onClick.AddListener(SetParentInactive);
    }

    void SetParentInactive()
    {
        // Set the parent GameObject to inactive
        parentObject.SetActive(false);
    }
}