using UnityEngine;

public class ColorCycler : MonoBehaviour
{
    public float changeInterval = 2f;  // Seconds before switching colors
    
    private Renderer rend;
    private int currentIndex = 0;

    private Color[] colors = new Color[]
    {
        Color.blue,
        Color.green,
        Color.red
    };

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = colors[0];
        }
        InvokeRepeating(nameof(CycleColor), changeInterval, changeInterval);
    }

    void CycleColor()
    {
        currentIndex = (currentIndex + 1) % colors.Length;
        rend.material.color = colors[currentIndex];
    }
}
