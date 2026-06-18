using UnityEngine;
using UnityEngine.EventSystems;

public class VRRayDebug : MonoBehaviour
{
    public float maxDistance = 10f;      // How far the ray checks
    public LayerMask uiLayer;            // Layer of your canvas/UI

    void Update()
    {
        // Cast a ray from the controller forward
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, uiLayer))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }

        // Optional: Visualize the ray
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red);
    }
}