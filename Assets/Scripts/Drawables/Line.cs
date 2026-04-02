using UnityEngine;

public class Line : Drawable
{
    [SerializeField] bool generateOnLoad = false;

    [SerializeField] Transform lineTransform;
    [SerializeField] Transform colliderTransform;

    public float Length => lineTransform.localScale.x;

    [SerializeField] float length = 1;

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
        transform.position = position;
        lineTransform.localScale = new(length, 1, 1);
    }

    public override void GenerateColliders()
    {
        float length = lineTransform.localScale.x;

        colliderTransform.localPosition = new(length / 2, 0f, 0f);
        colliderTransform.localScale = new(length + 0.25f, 1f, 1f);
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
        SetPositionLength(transform.position, length);
        GenerateColliders();
    }
}
