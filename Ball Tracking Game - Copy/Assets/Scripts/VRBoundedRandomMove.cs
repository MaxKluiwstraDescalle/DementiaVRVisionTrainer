using UnityEngine;
using System.Collections; // Required for IEnumerator

public class VRBoundedRandomMove : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 randomDirection;

    // Boundary range relative to spawn position
    public float xRange = 2f;   // How far left/right it can go
    public float yRange = 1.5f; // How far up/down it can go
    public float zRange = 4f;   // How far forward it can go (no backward movement)

    private Vector3 spawnPosition; // Stores the ball's initial position

    // Scaling variables
    private Vector3 originalScale;
    public float enlargedScaleFactor = 2f;
    public float reducedScaleFactor = 0.5f;

    void Start()
    {
        spawnPosition = transform.position; // Save where the ball starts
        originalScale = transform.localScale;
        ChangeDirection();
        InvokeRepeating("ChangeDirection", 0f, 2f); // Changes direction every 2 seconds
        StartCoroutine(ScaleRoutine());
    }

    void Update()
    {
        transform.Translate(randomDirection * speed * Time.deltaTime);
        CheckBounds();
    }

    void ChangeDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(0.2f, 1f); // Favor forward movement to avoid going backward

        randomDirection = new Vector3(x, y, z).normalized;
    }

    void CheckBounds()
    {
        Vector3 pos = transform.position;

        // Reverse direction when hitting bounds
        if (pos.x < spawnPosition.x - xRange || pos.x > spawnPosition.x + xRange) 
            randomDirection.x = -randomDirection.x;
        if (pos.y < spawnPosition.y - yRange || pos.y > spawnPosition.y + yRange) 
            randomDirection.y = -randomDirection.y;
        if (pos.z < spawnPosition.z || pos.z > spawnPosition.z + zRange) 
            randomDirection.z = -randomDirection.z;

        // Clamp position within bounds
        pos.x = Mathf.Clamp(pos.x, spawnPosition.x - xRange, spawnPosition.x + xRange);
        pos.y = Mathf.Clamp(pos.y, spawnPosition.y - yRange, spawnPosition.y + yRange);
        pos.z = Mathf.Clamp(pos.z, spawnPosition.z, spawnPosition.z + zRange);
        transform.position = pos;
    }

    IEnumerator ScaleRoutine()
    {
        while (true)
        {
            // Start at original size
            transform.localScale = originalScale;
            yield return new WaitForSeconds(5f);

            // Enlarge the sphere
            transform.localScale = originalScale * enlargedScaleFactor;
            yield return new WaitForSeconds(5f);

            // Return to original size
            transform.localScale = originalScale;
            yield return new WaitForSeconds(5f);

            // Reduce the sphere
            transform.localScale = originalScale * reducedScaleFactor;
            yield return new WaitForSeconds(5f);
        }
    }
}
