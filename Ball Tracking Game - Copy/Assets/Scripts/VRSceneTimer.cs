using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class VRSceneTimer : MonoBehaviour
{
    
    public float timeLimit = 60f; // Total time limit in seconds
    private float timeLeft;       // Remaining time
    private bool gameEnded = false;

    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI to display the timer
    public float returnDelay = 10f;
    private float returnTimer;
    public TextMeshProUGUI gameOverText; // Reference to the TextMeshPro UI to display Game Over text

    void Start()
    {
        // Initialize the timer
        timeLeft = timeLimit;

        // Optional: Check if the timerText reference is assigned
        if (timerText == null)
        {
            Debug.LogError("TimerText reference is missing. Please assign it in the Inspector.");
        }

        // Optional: Check if the gameOverText reference is assigned
        if (gameOverText == null)
        {
            Debug.LogError("GameOverText reference is missing. Please assign it in the Inspector.");
        }

        // Make the Game Over text hidden initially
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    void Update()
    {

         if (gameEnded) return;
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

    IEnumerator ReturnCountdown()
    {
        while (returnTimer > 0f)
        {
            if (gameOverText != null)
            {
                gameOverText.text =
                    $"Game Over!\n" +
                    $"Score: {LaserDetection.score}\n\n" +
                    $"Returning to menu in {Mathf.CeilToInt(returnTimer)}";
                gameOverText.gameObject.SetActive(true);
            }

            returnTimer -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Menu");
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
        //Debug.Log("Time is up! Showing game over...");
         if (gameEnded) return;
        gameEnded = true;
        
         Time.timeScale = 0f;
        AudioListener.pause = true;

         returnTimer = returnDelay;
        // Show "Game Over" message along with the score from LaserDetection
        if (gameOverText != null)
        {
            timerText.text = "";
            gameOverText.text = "Game Over!\nScore: " + LaserDetection.score;
            gameOverText.gameObject.SetActive(true);
        }

        // Optional: Wait for a few seconds before returning to the menu
        StartCoroutine(ReturnCountdown());
    }

    void LoadMenuScene()
    {
        // Load the Menu scene (replace "Menu" with the actual name of your menu scene)
        SceneManager.LoadScene("Menu"); // Replace "Menu" with your actual scene name
    }
}
