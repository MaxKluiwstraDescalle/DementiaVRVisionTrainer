using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class VRSceneTimer : MonoBehaviour
{
    
    public float timeLimit = 60f; // Total time limit in seconds
    private float timeLeft;       // Remaining time
    private bool gameEnded = false;

    private TextMeshProUGUI timerText;
    public float returnDelay = 10f;
    private float returnTimer;
    private TextMeshProUGUI gameOverText;

    void Start()
    {
        LaserDetection.ResetScore();
        timeLeft = timeLimit;

        if (timerText == null)
        {
            GameObject timerObject = GameObject.Find("TimerText");
            if (timerObject != null)
            {
                timerText = timerObject.GetComponent<TextMeshProUGUI>();
            }
        }

        if (gameOverText == null)
        {
            GameObject gameOverObject = GameObject.Find("GameOverText");
            if (gameOverObject != null)
            {
                gameOverText = gameOverObject.GetComponent<TextMeshProUGUI>();
            }
        }

        if (timerText == null)
        {
            Debug.LogError("Could not find a TextMeshProUGUI component on the object named 'TimerText'.");
        }

        if (gameOverText == null)
        {
            Debug.LogError("Could not find a TextMeshProUGUI component on the object named 'GameOverText'.");
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
                    $"Score: {LaserDetection.Score}\n\n" +
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
        GameObject.Find("PauseButton")?.SetActive(false);
        GameObject.Find("BackButton")?.SetActive(false);
         Time.timeScale = 0f;
        AudioListener.pause = true;

         returnTimer = returnDelay;
        if (gameOverText != null)
        {
            if (timerText != null)
            {
                timerText.text = "";
            }

            gameOverText.text = "Game Over!\nScore: " + LaserDetection.Score;
            gameOverText.gameObject.SetActive(true);
        }

        StartCoroutine(ReturnCountdown());
    }
}
