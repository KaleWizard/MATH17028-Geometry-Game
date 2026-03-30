using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Arc : Drawable
{
    [SerializeField] bool generateOnLoad = false;

    [SerializeField] CircleCollider2D outerFillCol;
    [SerializeField] CircleCollider2D centerCutoutCol;
    [SerializeField] PolygonCollider2D arcDividerCol;
    [SerializeField] Transform startCap;
    [SerializeField] Transform endCap;
    [Space]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int lineFidelity = 32;
    [Space]
    [SerializeField] int polyFidelity = 10;
    [SerializeField] float polyDistance = 1.25f;
    [Space]
    [SerializeField] float radius = 1;
    [SerializeField] float angle = 1;

    private void Start()
    {
        if (generateOnLoad)
        {
            GenerateColliders();
            UpdateLine();
        }
    }

    public void SetRotation(Vector3 direction)
    {
        transform.right = direction;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetArc(float r, float theta)
    {
        radius = r;
        angle = Mathf.Clamp(theta, 0f, Mathf.PI * 2);
        UpdateLine();
    }

    public void SetRadius(float r)
    {
        radius = r;
        UpdateLine();
    }

    public void SetArcLength(float theta)
    {
        angle = Mathf.Clamp(theta, 0f, Mathf.PI * 2);
        UpdateLine();
    }

    public override void GenerateColliders()
    {
        outerFillCol.radius = radius + 0.125f;
        centerCutoutCol.radius = radius - 0.125f;

        // Get arc cutout points
        Vector2[] path = new Vector2[polyFidelity];
        path[0] = Vector2.zero;

        float distance = radius * polyDistance;

        for (int i = 0; i < path.Length - 1; i++) 
        {
            float pointAngle = angle * i / (polyFidelity - 2);
            path[i + 1] = new Vector2(Mathf.Cos(pointAngle), Mathf.Sin(pointAngle)) * distance;
        }

        arcDividerCol.SetPath(0, path);
    }

    void UpdateLine()
    {
        Vector3[] path = new Vector3[lineFidelity];

        for (int i = 0; i < lineFidelity; i++)
        {
            float pointAngle = angle * i / (lineFidelity - 1);
            path[i] = new Vector3(Mathf.Cos(pointAngle), Mathf.Sin(pointAngle), 0f) * radius;
        }

        lineRenderer.positionCount = lineFidelity;
        lineRenderer.SetPositions(path);

        startCap.position = path[0];
        endCap.position = path[^1];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        angle = Mathf.Clamp(angle, 0f, Mathf.PI * 2);
        UpdateLine();
        GenerateColliders();
    }
#endif
}
