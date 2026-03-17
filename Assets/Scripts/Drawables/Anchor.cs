using UnityEngine;

public class Anchor : MonoBehaviour
{
    [SerializeField] Color baseColor = Color.white;
    [SerializeField] Color hoverColor = Color.orange;
    [SerializeField] Color selectColor = Color.red;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = baseColor;
        transform.localEulerAngles = Vector3.forward * AnchorManager.Instance.Rotation;
    }

    void LateUpdate()
    {
        transform.localEulerAngles = Vector3.forward * AnchorManager.Instance.Rotation;
    }

    public void OnHover()
    {
        sr.color = hoverColor;
    }

    public void OnSelect()
    {
        sr.color = selectColor;
    }

    public void OnDeselect()
    {
        sr.color = baseColor;
    }
}
