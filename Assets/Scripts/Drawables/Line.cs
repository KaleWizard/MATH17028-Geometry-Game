using UnityEngine;

public class Line : Drawable
{
    [SerializeField] bool generateOnLoad = false;

    [SerializeField] Transform lineTransform;
    [SerializeField] Transform colliderTransform;

    [SerializeField] LineBuilder lineBuilder;
    [SerializeField] LineRenderer lineRenderer;

    public float Length => lineTransform.localScale.x;

    [SerializeField] float length = 1;
    [SerializeField] float lineDistBetweenPoints = 0.1f;

    Vector3 OriginPos => originPos ?? Vector3.zero;
    Vector3? originPos = null;

    private void Start()
    {
        if (generateOnLoad)
        {
            GenerateColliders();

            DrawableManager.Instance.AddDrawable(this);
        }
    }

    public void SetRotation(Vector3 direction)
    {
        transform.right = direction;
    }

    public void SetPositionLength(Vector3 position, float length)
    {
        this.length = length;
        originPos ??= position;

        transform.position = position;

        lineRenderer.positionCount = Mathf.FloorToInt(length / lineDistBetweenPoints) + 1;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * lineDistBetweenPoints, 0, 0));
        }
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new(length, 0f, 0f));

        float back = (position - OriginPos).magnitude;
        float forward = length - back;
        lineBuilder.SetLength(back, forward);
    }

    public override void GenerateColliders()
    {
        colliderTransform.localPosition = new(length / 2, 0f, 0f);
        colliderTransform.localScale = new(length, 1f, 1f);
    }

    public bool IsValidPoint(Vector2 point)
    {
        Vector2 start = transform.position;
        Vector2 end = transform.position + transform.right * lineTransform.localScale.x;

        return Mathf.Min(start.x, end.x) - 0.01f < point.x && Mathf.Max(start.x, end.x) + 0.01f > point.x
            && Mathf.Min(start.y, end.y) - 0.01f < point.y && Mathf.Max(start.y, end.y) + 0.01f > point.y;
    }

    private void OnValidate()
    {
        if (!generateOnLoad) return;
        originPos = null;
        length = Mathf.Max(0, length);
        SetPositionLength(transform.position, length);
        GenerateColliders();
    }
}
