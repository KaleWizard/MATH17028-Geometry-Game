using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] float maxLength = 2f;
    [SerializeField] Transform drawPoint;

    public void Hover()
    {
        transform.position = GameUtils.WorldMousePosition();
    }

    public float Measure(Vector3 anchor)
    {
        transform.position = anchor;
        Vector3 direction = (Vector3)GameUtils.WorldMousePosition() - anchor;
        transform.right = direction;

        float dist = Mathf.Clamp(direction.magnitude, 0f, maxLength);
        drawPoint.localPosition = new Vector3(dist, 0f, 0f);
        return dist;
    }

    public void DrawingPosition(Vector3 anchor, float radius)
    {
        transform.position = anchor;
        Vector3 direction = (Vector3)GameUtils.WorldMousePosition() - anchor;
        transform.right = direction;

        radius = Mathf.Clamp(radius, 0f, maxLength);
        drawPoint.localPosition = new Vector3(radius, 0f, 0f);
    }
}
