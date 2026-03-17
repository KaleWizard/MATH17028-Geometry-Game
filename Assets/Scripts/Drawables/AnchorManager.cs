using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnchorManager : SingletonBehaviour<AnchorManager>
{
    public List<Anchor> anchors = new();

    float speed = 30f;
    public float Rotation => rotationTimer * speed;
    float rotationTimer = 0;

    GameObject anchorPrefab;


    public void SpawnAnchor(Vector3 position)
    {

    }



    void Start()
    {
        SceneManager.sceneLoaded += (_,_) => OnSceneLoad();
        GetAnchors();

        // Load anchor prefab
        anchorPrefab = Resources.Load<GameObject>("Prefabs/Anchor");
    }

    void Update()
    {
        rotationTimer += Time.deltaTime;
        if (rotationTimer > 360f)
        {
            rotationTimer -= 360f;
        }
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
}
