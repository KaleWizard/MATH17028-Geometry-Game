using System.Net;
using UnityEngine;

public abstract class Drawable : MonoBehaviour
{
    public abstract void GenerateColliders();

    public static Vector3? IntersectLines(Line a, Line b)
    {
        // Calculation via https://stackoverflow.com/questions/563198/how-do-you-detect-where-two-line-segments-intersect

        Vector2 originA = a.transform.position;
        Vector2 dirA = a.transform.right.normalized;
        float lenA = a.Length;

        Vector2 originB = b.transform.position;
        Vector2 dirB = b.transform.right.normalized;
        float lenB = b.Length;

        float crossDir = Cross(dirA, dirB);

        if (Mathf.Abs(crossDir) < 0.05f) return null;

        float valA = Cross(originB - originA, dirB) / crossDir;
        float valB = Cross(originB - originA, dirA) / crossDir;

        if (0 < valA && valA < lenA && 0 < valB && valB < lenB)
            return originA + valA * dirA;

        return null;
    }

    static float Cross(Vector2 a, Vector2 b)
    {
        return a.x * b.y - a.y * b.x;
    }

    public static (Vector3?, Vector3?) IntersectArcs(Arc a, Arc b)
    {
        float dist = Vector3.Distance(a.transform.position, b.transform.position);

        if (dist < 0.02f || a.Radius + b.Radius < dist)
            return (null, null);

        // Calculation via https://math.stackexchange.com/questions/256100/how-can-i-find-the-points-at-which-two-circles-intersect

        Vector2 posA = a.transform.position;
        Vector2 posB = b.transform.position;

        float sqrSum = a.Radius * a.Radius + b.Radius * b.Radius;
        float sqrDiff = a.Radius * a.Radius - b.Radius * b.Radius;

        float distSqr = dist * dist;

        Vector2 midPoint = (posA + posB) / 2 + (sqrDiff / (2 * distSqr)) * (posB - posA);
        Vector2 pm = (Mathf.Sqrt(2 * sqrSum / distSqr - (sqrDiff * sqrDiff) / (distSqr * distSqr) - 1) / 2) * new Vector2(posB.y - posA.y, posA.x - posB.x);

        Vector3? p1 = a.IsValidPoint(midPoint + pm) && b.IsValidPoint(midPoint + pm) ? midPoint + pm : null;
        Vector3? p2 = a.IsValidPoint(midPoint - pm) && b.IsValidPoint(midPoint - pm) ? midPoint - pm : null;

        return (p1, p2);
    }
    public static (Vector3?, Vector3?) IntersectLineArc(Arc arc, Line line) => IntersectLineArc(line, arc);

    public static (Vector3?, Vector3?) IntersectLineArc(Line line, Arc arc)
    {
        // Calculation via https://mathworld.wolfram.com/Circle-LineIntersection.html
        Vector2 start = line.transform.position - arc.transform.position;
        Vector2 end = line.transform.position + line.transform.right * line.Length - arc.transform.position;

        float run = end.x - start.x;
        float rise = end.y - start.y;
        float distSqr = run * run + rise * rise;
        float cross = Cross(start, end);

        float det = arc.Radius * arc.Radius * distSqr - cross * cross;

        if (det < 0) return (null, null);
        
        Vector2 midPoint = cross / distSqr * new Vector2(rise, -run) + (Vector2) arc.transform.position;
        Vector2 pm = new(Mathf.Sign(rise) * run * Mathf.Sqrt(det) / distSqr, Mathf.Abs(rise) * Mathf.Sqrt(det) / distSqr);

        Debug.Log((line.gameObject.name, midPoint + pm, midPoint - pm));
        Debug.DrawLine(midPoint + pm, midPoint - pm, Color.black);

        Vector3? p1 = line.IsValidPoint(midPoint + pm) && arc.IsValidPoint(midPoint + pm) ? midPoint + pm : null;
        Vector3? p2 = line.IsValidPoint(midPoint - pm) && arc.IsValidPoint(midPoint - pm) ? midPoint - pm : null;

        return (p1, p2);
    }
}
