using UnityEngine;

public class PrintAngle : MonoBehaviour
{
    void Update()
    {
        Vector2 direction = GameUtils.WorldMousePosition() - Vector2.zero;
        float angle = Vector2.SignedAngle(Vector2.left, direction) + 180f;
        Debug.Log(angle);
        transform.up = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
}
