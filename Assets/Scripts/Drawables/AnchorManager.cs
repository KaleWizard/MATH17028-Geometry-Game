using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnchorManager : SingletonBehaviour<AnchorManager>
{
    public static float Rotation => Instance.rotationTimer * Instance.speed;
    public static List<Anchor> Anchors => Instance.anchors;

    Anchor anchorPrefab;

    List<Anchor> anchors = new();
    Anchor hovered = null;

    float speed = 30f;
    float rotationTimer = 0;

    public static void SpawnAnchor(Vector3 position)
    {
        Anchor inScene = Instantiate(Instance.anchorPrefab, position, Quaternion.identity);
        Anchors.Add(inScene);
    }

    public static Anchor GetHoveredAnchor(float minDistance = 0.3f)
    {
        Anchor hovered = null;

        Vector2 mousePosition = GameUtils.WorldMousePosition();
        foreach (var anchor in Anchors)
        {
            float distance = Vector2.Distance(mousePosition, anchor.transform.position);
            if (distance < minDistance)
            {
                hovered = anchor;
                minDistance = distance;
            }
        }
        return hovered;
    }

    void Start()
    {
        SceneManager.sceneLoaded += (_,_) => OnSceneLoad();
        GetAnchors();

        // Load anchor prefab
        anchorPrefab = Resources.Load<Anchor>("Prefabs/Anchor");
    }

    void Update()
    {
        SpinAnchors();
        UpdateHoveredAnchor();
    }

    void OnSceneLoad()
    {
        anchors.Clear();
        GetAnchors();
    }

    void GetAnchors()
    {
        var sceneAnchors = FindObjectsByType<Anchor>(FindObjectsSortMode.None);
        foreach (var anchor in sceneAnchors)
            anchors.Add(anchor);
    }

    void SpinAnchors()
    {
        rotationTimer += Time.deltaTime;
        if (rotationTimer > 360f)
        {
            rotationTimer -= 360f;
        }
    }

    void UpdateHoveredAnchor()
    {
        Anchor newHovered = GetHoveredAnchor();
        if (newHovered && hovered != newHovered)
        {
            if (hovered) hovered.OnDeselect();
            hovered = newHovered;
            hovered.OnSelect();
        }

        if (hovered && !newHovered)
        {
            hovered.OnDeselect();
            hovered = null;
        }
    }
}
