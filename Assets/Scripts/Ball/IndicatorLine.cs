using Unity.Mathematics;
using UnityEngine;

public class IndicatorLine : MonoBehaviour
{
    private void OnValidate()
    {
        var path = new Vector3[32];

        for (int i = 0; i < path.Length; i++)
        {
            path[i] = (new Vector3(Mathf.Sin(2 * Mathf.PI * i / path.Length), Mathf.Cos(2 * Mathf.PI * i / path.Length))) / 2;
        }

        var lr = GetComponent<LineRenderer>();
        lr.positionCount = path.Length;
        lr.SetPositions(path);
    }
}
