using UnityEngine;
using UnityEngine.UI;

public class volumeScript : MonoBehaviour
{
    public Sprite[] volumeStates;
    private Image buttonImage;
    private int currentStateIndex = 3;
    private Button button;
    private AudioManager audioManager;
    
    void Start()
    {
        // Get audio manager
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Get the Image component of the Button
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // Ensure the Button has an Image component
        if (buttonImage == null)
        {
            Debug.LogError("VolumeButton GameObject needs an Image component.");
            enabled = false; // Disable the script if no Image is found
            return;
        }

        // Ensure we have the correct number of states
        if (volumeStates.Length != 4)
        {
            Debug.LogError("VolumeStates array must have exactly 4 elements.");
            enabled = false; // Disable the script if the array is not the correct size
            return;
        }

        // Set the initial image
        UpdateImage();

        // Add a listener to the button's onClick event
        button.onClick.AddListener(CycleVolumeState);

    }

    void CycleVolumeState()
    {
        // Increment the state index and wrap around if it exceeds the array bounds
        currentStateIndex = (currentStateIndex + 1) % volumeStates.Length;

        // Update the button's image
        UpdateImage();

        // Setting the new volume
        if (currentStateIndex == 0) // Mute 
        {
            audioManager.SetVolume(0f);
        }
        else if (currentStateIndex == 1) // Low
        {
            audioManager.SetVolume(0.33f);
        }
        else if (currentStateIndex == 2) // Medium
        {
            audioManager.SetVolume(0.66f);
        }
        else if (currentStateIndex == 3) // High
        {
            audioManager.SetVolume(1.00f);
        }
        else
        {
            Debug.Log("Invalid volume state index");
        }
    }

    void UpdateImage()
    {
        if (buttonImage != null && volumeStates.Length > 0 && currentStateIndex < volumeStates.Length)
        {
            buttonImage.sprite = volumeStates[currentStateIndex];
        }
    }
}
