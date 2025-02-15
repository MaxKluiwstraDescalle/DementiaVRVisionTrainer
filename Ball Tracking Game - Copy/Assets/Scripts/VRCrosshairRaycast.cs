using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VRCrosshairRaycast : MonoBehaviour
{
    public float interactionTime = 2f; // Time to interact with the object
    private float gazeTimer = 0f;

    public RectTransform crosshair; // Reference to the crosshair RectTransform
    public Camera vrCamera; // The VR camera that renders the world
    public TMP_Text rulesText; // Reference to the rules text UI

    private Ray ray;
    private RaycastHit hit;

    void Update()
    {
        // Get the screen position of the crosshair
        Vector3 crosshairScreenPosition = RectTransformUtility.WorldToScreenPoint(vrCamera, crosshair.position);

        // Create a ray from the camera through the crosshair's screen position
        ray = vrCamera.ScreenPointToRay(crosshairScreenPosition);

        // Debug: Draw the ray in the editor
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green);

        if (Physics.Raycast(ray, out hit))
        {
            // Check what the raycast hits based on tags
            if (hit.collider.CompareTag("MainGameBox"))
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= interactionTime)
                {
                    LoadGameScene("MainGame");
                }
            }
            else if (hit.collider.CompareTag("PlaceboBox"))
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= interactionTime)
                {
                    LoadGameScene("Placebo");
                }
            }
            else if (hit.collider.CompareTag("RulesBox"))
            {
                gazeTimer += Time.deltaTime;
                if (gazeTimer >= interactionTime)
                {
                    ShowRules();
                }
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

    void LoadGameScene(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }

    void ShowRules()
    {
        if (rulesText != null)
        {
            rulesText.text = "<b>Rules:\n1. Pick Game \n2. Game 1: Explore the image that is \ndisplayed around you \n3.Game 2: Track the black orb with the + \nat the center of your screen\n\n<b> Game Made by: \n Max-Emilien KD";
            rulesText.gameObject.SetActive(true);
        }
    }
}
