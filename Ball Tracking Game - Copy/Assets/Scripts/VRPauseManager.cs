using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class VRPauseManager : MonoBehaviour
{
    private bool isPaused = false;
    public TextMeshProUGUI pText; // Assign in Inspector
    public TextMeshProUGUI pausedText; // Assign in Inspector
    
    public string mainMenuSceneName = "Menu"; // exact scene name
    public void BackButton()
    {
         // Make sure game is unpaused before leaving
        Time.timeScale = 1f;
        AudioListener.pause = false;

        SceneManager.LoadScene(mainMenuSceneName);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pText.text = "Resume";
            Time.timeScale = 0f;         // Stop game time
            AudioListener.pause = true;   // Pause audio

            if (pausedText != null)
                pausedText.gameObject.SetActive(true); // Show paused text
        }
        else
        {
            pText.text = "Pause";
            Time.timeScale = 1f;         // Resume game time
            AudioListener.pause = false;  // Resume audio

            if (pausedText != null)
                pausedText.gameObject.SetActive(false); // Hide paused text
        }
    }
}
