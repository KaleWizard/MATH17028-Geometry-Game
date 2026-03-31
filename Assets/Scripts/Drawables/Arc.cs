using Unity.Mathematics;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Arc : Drawable
{
    [SerializeField] bool generateOnLoad = false;

    [SerializeField] CompositeCollider2D compositeCol;
    [SerializeField] CircleCollider2D outerFillCol;
    [SerializeField] CircleCollider2D centerCutoutCol;
    [SerializeField] PolygonCollider2D arcDividerCol;
    [SerializeField] CircleCollider2D startCapCol;
    [SerializeField] CircleCollider2D endCapCol;
    [Space]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] int lineFidelity = 32;
    [Space]
    [SerializeField] int polyFidelity = 10;
    [SerializeField] float polyDistance = 1.25f;
    [Space]
    [SerializeField] float radius = 1;
    [SerializeField] float angle = 1;

    public float Radius => radius;

    float minAngle;
    float maxAngle;

    private void Start()
    {
        if (generateOnLoad)
        {
            GenerateColliders();
            UpdateLine();

            DrawableManager.Instance.AddDrawable(this);
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

    public void SetArc(float r, float min, float max)
    {
        radius = r;
        minAngle = min;
        maxAngle = max;
        angle = Mathf.Clamp(max - min, 0f, Mathf.PI * 2);
        UpdateLine();
    }

    public void SetRadius(float r)
    {
        radius = r;
        UpdateLine();
    }

    public void SetArcLength(float min, float max)
    {
        minAngle = min;
        maxAngle = max;
        angle = Mathf.Clamp(max - min, 0f, Mathf.PI * 2);
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
            float pointAngle = angle + (math.TAU - angle) * i / (polyFidelity - 2);
            path[i + 1] = new Vector2(Mathf.Cos(pointAngle), Mathf.Sin(pointAngle)) * distance;
        }

        arcDividerCol.SetPath(0, path);
        compositeCol.GenerateGeometry();
    }

    public bool IsValidPoint(Vector2 point)
    {
        return IsValidDirection(point - (Vector2) transform.position);
    }

    public bool IsValidDirection(Vector2 direction)
    {
        float theta = (Vector2.SignedAngle(Vector2.left, direction) + 180f - transform.localEulerAngles.z) * Mathf.Deg2Rad;

        return InRange(theta, 0, angle) ||
            InRange(theta,  math.TAU, angle + math.TAU) ||
            InRange(theta, -math.TAU, angle - math.TAU);
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

        startCapCol.offset = path[0].normalized * radius;
        endCapCol.offset = path[^1].normalized * radius;
    }

    bool InRange(float value, float min, float max)
    {
        return min < value && value < max;
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
