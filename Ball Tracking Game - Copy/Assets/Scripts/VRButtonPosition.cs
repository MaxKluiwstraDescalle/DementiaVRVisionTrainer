
using UnityEngine;
using System.Collections;

public class VRButtonPosition : MonoBehaviour
{
    public Transform vrCamera;

    [Header("Position")]
    public float maxRise = 0.12f;
    public float lookDownAngle = 30f;
    public float smoothSpeed = 3f;

    private float startingPitch;
    private Vector3 startingPosition;
    private bool initialized = false;

    IEnumerator Start()
    {
        yield return null;
        yield return new WaitForEndOfFrame();

        startingPitch = vrCamera.localEulerAngles.x;

        if (startingPitch > 180f)
            startingPitch -= 360f;

        startingPosition = transform.localPosition;
        initialized = true;
    }

    void LateUpdate()
    {
        if (!initialized) return;

        float currentPitch = vrCamera.localEulerAngles.x;

        if (currentPitch > 180f)
            currentPitch -= 360f;

        float pitchDifference = currentPitch - startingPitch;

        // Only respond to looking downward
        float lookAmount = Mathf.Clamp01(
            pitchDifference / lookDownAngle
        );

        // Small upward movement
        Vector3 targetPosition = startingPosition;
        targetPosition.y += maxRise * lookAmount;

        transform.localPosition = Vector3.Lerp(
            transform.localPosition,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );
    }
}
