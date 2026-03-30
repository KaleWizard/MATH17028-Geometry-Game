using UnityEngine;

public class StraightEdge : MonoBehaviour
{
    public float Length => length;

    [SerializeField] Transform visuals;
    [SerializeField] SpriteRenderer baseEdge;

    float length;

    public Vector3 AnchorA => positionA;
    public Vector3 AnchorB => positionB;

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
        transform.eulerAngles = Vector3.zero;
    }

    public bool IsValidAnchor(Anchor anchor)
    {
        return (anchorsSet == 0) || (anchorsSet == 1 && Vector3.Distance(anchor.transform.position, positionA) > 0.25f);
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
        transform.position = mousePosition;
    }

    void SetPosRotOneAnchor()
    {
        transform.position = positionA;
        Vector2 mousePosition = GameUtils.WorldMousePosition();
        Vector2 direction = mousePosition - (Vector2) positionA;
        transform.right = direction;
    }

    void SetPosRotTwoAnchors()
    {
        transform.position = positionA;
        Vector2 direction = positionB - positionA;
        transform.right = direction;
    }
}
