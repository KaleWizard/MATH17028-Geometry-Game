using UnityEngine;

public class StraightEdge : MonoBehaviour
{
    public float Length => length;

    [SerializeField] Transform visuals;
    [SerializeField] SpriteRenderer baseEdge;

    float length;

    Vector3 positionA;
    Vector3 positionB;

    int anchorsSet = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        length = baseEdge.transform.localScale.x;
    }

    void Update()
    {
        SetPositionRotation();
    }

    public void SetAnchor(Vector3 position)
    {
        if (anchorsSet == 0)
            positionA = position;
        else if (anchorsSet == 1)
            positionB = position;

        anchorsSet++;
    }

    public void ResetAnchors()
    {
        anchorsSet = 0;
        visuals.eulerAngles = Vector3.zero;
    }

    void SetPositionRotation()
    {
        if (anchorsSet == 0) 
            SetPositionHover();
        else if (anchorsSet == 1) 
            SetPosRotOneAnchor();
        else 
            SetPosRotTwoAnchors();
    }

    void SetPositionHover()
    {
        Vector2 mousePosition = GameUtils.WorldMousePosition();
        visuals.position = mousePosition;
    }

    void SetPosRotOneAnchor()
    {
        visuals.position = positionA;
        Vector2 mousePosition = GameUtils.WorldMousePosition();
        Vector2 direction = mousePosition - (Vector2) positionA;
        visuals.right = direction;
    }

    void SetPosRotTwoAnchors()
    {
        visuals.position = positionA;
        Vector2 direction = positionB - positionA;
        visuals.right = direction;
    }
}
