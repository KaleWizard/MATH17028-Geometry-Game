using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawableManager : SingletonBehaviour<DrawableManager>
{
    List<Line> lines = new();
    List<Arc> arcs = new();

    public void AddDrawable(Line line)
    {
        lines.Add(line);
    }

    public void AddDrawable(Arc arc)
    {
        arcs.Add(arc);
    }

    public void TryAddAnchors(Line line)
    {
        // Line-Line intersections
        foreach (var other in lines)
        {
            var p = Drawable.IntersectLines(line, other);
            if (p != null)
                AnchorManager.SpawnAnchor((Vector3) p);
        }

        // Line-Arc intersections
        foreach (var other in arcs)
        {
            var (p1, p2) = Drawable.IntersectLineArc(line, other);
            if (p1 != null)
                AnchorManager.SpawnAnchor((Vector3)p1);
            if (p2 != null)
                AnchorManager.SpawnAnchor((Vector3)p2);
        }
    }

    public void TryAddAnchors(Arc arc)
    {
        // Arc-Arc intersections
        foreach (var other in arcs)
        {
            var (p1, p2) = Drawable.IntersectArcs(arc, other);
            if (p1 != null)
                AnchorManager.SpawnAnchor((Vector3)p1);
            if (p2 != null)
                AnchorManager.SpawnAnchor((Vector3)p2);
        }

        // Line-Arc intersections
        foreach (var other in lines)
        {
            var (p1, p2) = Drawable.IntersectLineArc(arc, other);
            if (p1 != null)
                AnchorManager.SpawnAnchor((Vector3)p1);
            if (p2 != null)
                AnchorManager.SpawnAnchor((Vector3)p2);
        }
    }

    private void Start()
    {
        SceneManager.sceneUnloaded += (_) => ClearLists();
    }

    void ClearLists()
    {
        lines.Clear();
        arcs.Clear();
    }
}
