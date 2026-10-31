using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro; // For TextMeshPro support

public class CrosshairDetection : MonoBehaviour
{
    public int score = 0; // Current score
    public Camera vrCamera; // Reference to the VR camera
    public float detectionDistance = 100f; // Raycast max distance
    public TextMeshProUGUI scoreText; // Reference to TextMeshPro score UI

    private int frameCount = 0; // Frame counter

    void Update()
    {
        // Create a ray starting from the camera going forward
        Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, detectionDistance))
        {
            // Check if the hit object has the "Ball" tag
            if (hit.collider.CompareTag("Ball"))
            {
                frameCount++; // Increment the frame count while the ball is being targeted

                // If we've hit 10 frames, increment the score
                if (frameCount >= 10)
                {
                    score++;
                    scoreText.text = "Score: " + score; // Update the UI
                    frameCount = 0; // Reset frame counter
                }
            }
        }
        else
        {
            frameCount = 0; // Reset frame counter if not hitting the ball
        }
    }
}
