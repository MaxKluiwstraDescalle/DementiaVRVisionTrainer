using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VRSceneTimer : MonoBehaviour
{
    public float timeLimit = 60f; // Total time limit in seconds
    private float timeLeft;       // Remaining time

    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI to display the timer

    void Start()
    {
        // Initialize the timer
        timeLeft = timeLimit;

        // Optional: Check if the timerText reference is assigned
        if (timerText == null)
        {
            Debug.LogError("TimerText reference is missing. Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        // Decrease the time left
        timeLeft -= Time.deltaTime;

        // Update the timer display
        UpdateTimerDisplay();

        // If time runs out, trigger the end of the scene
        if (timeLeft <= 0f)
        {
            EndScene();
        }
    }

    void UpdateTimerDisplay()
    {
        // Convert time to minutes and seconds
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        // Format the time string (MM:SS)
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update the UI text
        if (timerText != null)
        {
            timerText.text = "Time Left: " + timeString;
        }
    }

    void EndScene()
    {
        Debug.Log("Time is up! Returning to the menu...");

        // Load the Menu scene (replace "MenuScene" with the actual name of your menu scene)
        SceneManager.LoadScene("Menu"); // Replace "MenuScene" with your actual scene name
    }
}
