using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR;

public class VRLineRaycast : MonoBehaviour
{
    public float interactionTime = 2f; // Time required to select an object
    private float gazeTimer = 0f;

    public Transform controllerTransform; // Reference to the VR controller
    public TMP_Text rulesText; // UI text to display rules
    public LineRenderer lineRenderer; // LineRenderer for ray visualization

    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; // Start and end points
            lineRenderer.enabled = true;    // Make sure it's visible
        }
    }

    void Update()
    {
        if (controllerTransform == null)
        {
            Debug.LogError("Controller Transform not assigned!");
            return;
        }

        // Cast a ray from the controller's position forward
        ray = new Ray(controllerTransform.position, controllerTransform.forward);

        // Draw the ray using the LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, ray.origin); // Start at the controller
            lineRenderer.SetPosition(1, ray.origin + ray.direction * 10f); // Extend forward
        }

        if (Physics.Raycast(ray, out hit))
        {
            lineRenderer.SetPosition(1, hit.point); // Adjust line end to hit point

            // Check what the raycast hits based on tags
            if (hit.collider.CompareTag("MainGameBox"))
            {
                HandleGazeInteraction("MainGame");
            }
            else if (hit.collider.CompareTag("PlaceboBox"))
            {
                HandleGazeInteraction("Placebo");
            }
            else if (hit.collider.CompareTag("RulesBox"))
            {
                HandleGazeInteraction("Rules");
            }
            else if (hit.collider.CompareTag("EasyGameBox"))
            {
                HandleGazeInteraction("EasyGame");
            }
            else if (hit.collider.CompareTag("MediumGameBox"))
            {
                HandleGazeInteraction("MediumGame");
            }
            else if (hit.collider.CompareTag("HardGameBox"))
            {
                HandleGazeInteraction("HardGame");
            }
            else
            {
                gazeTimer = 0f; // Reset gaze timer if no relevant object is hit
            }
        }
        else
        {
            gazeTimer = 0f; // Reset timer if the raycast hits nothing
        }
    }

    void HandleGazeInteraction(string target)
    {
        gazeTimer += Time.deltaTime;
        if (gazeTimer >= interactionTime)
        {
            if (target == "Rules")
            {
                ShowRules();
            }
            else
            {
                LoadGameScene(target);
            }
        }
    }

    void LoadGameScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    void ShowRules()
    {
        if (rulesText != null)
        {
            rulesText.text = "<b>Rules:</b>\n1. Pick a game by pointing the laser pointer.\n2. Game 1: Explore the displayed image\n3. Games 2-5: Track the black orb with the laser pointer\nGames 2: Artistic Freedom\nGame 3: Easy\nGame 4: Medium\nGame 5: Hard\n\n<b>Game Made by:</b> Max-Emilien KD";
            rulesText.gameObject.SetActive(true);
        }
    }
}
