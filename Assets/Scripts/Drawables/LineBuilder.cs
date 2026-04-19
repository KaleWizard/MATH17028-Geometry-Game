using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineBuilder : MonoBehaviour
{
    [SerializeField] AnimationCurve widthCurve = new();

    LineRenderer lineRenderer;

    float width;

    public void SetLength(float start, float end)
    {
        if (!lineRenderer)
            OnWakeup();

        float startWidth = widthCurve.Evaluate(start);
        float endWidth = widthCurve.Evaluate(end);

        AnimationCurve newCurve = new();

        newCurve.AddKey(new(0, startWidth / width,0,0));
        newCurve.AddKey(new(1, endWidth / width,0,0));

        foreach (var key in widthCurve.keys)
        {
            float time = math.remap(start, end, 0, 1, key.time);
            if (0 < time && time < 1)
            {
                newCurve.AddKey(new(time, key.value / width, 0, 0));
            }
        }

        lineRenderer.widthCurve = newCurve;
    }

    private void Awake()
    {
        if (!lineRenderer)
            OnWakeup();
    }

    void OnWakeup()
    {
        lineRenderer = GetComponent<LineRenderer>();
        foreach (var key in widthCurve.keys)
            if (key.value > width)
                width = key.value;
    }

    private void OnValidate()
    {
        foreach (var key in widthCurve.keys)
            if (key.value > width)
                width = key.value;
    }
}
