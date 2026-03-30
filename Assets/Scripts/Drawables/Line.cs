using UnityEngine;

public class Line : Drawable
{
    [SerializeField] bool generateOnLoad = false;

    [SerializeField] Transform lineTransform;
    [SerializeField] Transform colliderTransform;

    private void Start()
    {
        if (generateOnLoad) GenerateColliders();
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
}
