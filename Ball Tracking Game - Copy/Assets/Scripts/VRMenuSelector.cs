using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VRMenuSelector : MonoBehaviour
{
    [Header("Rules Panels")]
    public GameObject sideRulesPanel;   // always-on-left rules
    public GameObject popupRulesPanel;  // rules shown before starting

    public TMP_Text popupRulesText;

    private string selectedScene;

    // LEFT-SIDE "RULES" BUTTON
    public void ToggleSideRules()
    {
        if (sideRulesPanel == null) return;

        sideRulesPanel.SetActive(!sideRulesPanel.activeSelf);
    }

    // GAME BUTTONS
    public void ShowPopupRulesForGame(string sceneName)
    {
        selectedScene = sceneName;

        // Make sure the side panel does NOT interfere
        if (sideRulesPanel != null)
            sideRulesPanel.SetActive(false);

        if (popupRulesPanel != null)
            popupRulesPanel.SetActive(true);

        if (popupRulesText != null)
        {
            popupRulesText.text =
                "<b>Rules:</b>\n" +
                "1. Pick a game from the menu.\n" +
                "2. Base Game: Explore the displayed image\n" +
                "3. Other games: Track the sphere with the laser pointer\n" +
                "4. Tracking the ball with both laser pointers gives bonus score.";
        }
    }

    // PLAY BUTTON (popup)
    public void PlayGame()
    {
        if (string.IsNullOrEmpty(selectedScene))
        {
            Debug.LogWarning("Play pressed but no game selected!");
            return;
        }

        SceneManager.LoadScene(selectedScene);
    }

    // BACK / CLOSE POPUP
    public void ClosePopupRules()
    {
        if (popupRulesPanel != null)
            popupRulesPanel.SetActive(false);
    }
}
