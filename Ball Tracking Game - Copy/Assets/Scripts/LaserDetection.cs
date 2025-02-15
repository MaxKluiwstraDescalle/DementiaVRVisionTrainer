using System.Collections;
using UnityEngine;
using TMPro; // For TextMeshPro UI

public class LaserDetection : MonoBehaviour
{
    public static int score = 0; // ✅ Shared score across both lasers
    public float laserDistance = 2000f; // Max laser distance
    public TextMeshProUGUI scoreText; // UI score display
    public LineRenderer lineRenderer; // Laser renderer
    public LayerMask detectionLayer = ~0; // Detect all layers for debugging
    public float scoreInterval = 0.5f; // Time before scoring again

    private Coroutine scoreCoroutine = null; // Track coroutine

    void Start()
    {
        score = 0;
        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2; // Ensure two points for the line
        }
        else
        {
            Debug.LogError("LineRenderer is missing on: " + gameObject.name);
        }
    }

    void Update()
    {
        if (lineRenderer == null) return;

        Vector3 startPoint = transform.position;
        Vector3 direction = transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(startPoint, direction, out hit, laserDistance, detectionLayer))
        {
            lineRenderer.SetPosition(0, startPoint);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Ball"))
            {
                if (scoreCoroutine == null) // Only start scoring if not already running
                {
                    scoreCoroutine = StartCoroutine(ScoreWhileTargeted());
                }
                return;
            }
        }

        // If the laser is NOT hitting the ball, stop scoring
        StopScoring();
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, startPoint + direction * laserDistance);
    }

    IEnumerator ScoreWhileTargeted()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreInterval); // Wait before adding score
            score++;

            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
        }
    }

    void StopScoring()
    {
        if (scoreCoroutine != null)
        {
            StopCoroutine(scoreCoroutine);
            scoreCoroutine = null;
        }
    }
}
