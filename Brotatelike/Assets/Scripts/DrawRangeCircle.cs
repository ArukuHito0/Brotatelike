using UnityEngine;

public class DrawRangeCircle : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private int segments;
    [SerializeField] private float linewidth;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.useWorldSpace = false;
        lineRenderer.loop = true;
        lineRenderer.positionCount = segments;
        lineRenderer.startWidth = linewidth;
        lineRenderer.endWidth = linewidth;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle();
    }

    private void DrawCircle()
    {
        float deltaAngle = 360f / segments;
        for (int i = 0; i < segments; i++)
        {
            float angle = i * deltaAngle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * player.Range;
            float y = Mathf.Sin(angle) * player.Range;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
