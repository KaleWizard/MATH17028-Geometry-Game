using UnityEngine;
using UnityEngine.UIElements;

public class Compass : MonoBehaviour
{
    [SerializeField] float maxLength = 2f;
    [SerializeField] Transform drawPoint;

    public Vector3 Center => transform.position;
    public float Span => drawPoint.localPosition.x;


    public void Hover(float length = -1)
    {
        if (length < 0) length = maxLength;
        transform.position = GameUtils.WorldMousePosition();
        drawPoint.localPosition = length * Vector3.right;
    }

    public Vector3 Measure(Vector3 anchor)
    {
        transform.position = anchor;
        Vector3 direction = (Vector3)GameUtils.WorldMousePosition() - anchor;
        transform.right = direction;

        float dist = Mathf.Clamp(direction.magnitude, 0.25f, maxLength);
        drawPoint.localPosition = new Vector3(dist, 0f, 0f);
        return drawPoint.position;
    }

    public Vector3 DrawingPosition(Vector3 anchor, float radius)
    {
        transform.position = anchor;
        Vector3 direction = (Vector3)GameUtils.WorldMousePosition() - anchor;
        transform.right = direction;

        radius = Mathf.Clamp(radius, 0.25f, maxLength);
        drawPoint.localPosition = new Vector3(radius, 0f, 0f);
        return direction;
    }

    public bool IsValidAnchor(Vector3 anchor)
    {
        float dist = Vector3.Distance(anchor, transform.position);
        return 0.25f < dist && dist < maxLength + 0.15f;
    }
}
