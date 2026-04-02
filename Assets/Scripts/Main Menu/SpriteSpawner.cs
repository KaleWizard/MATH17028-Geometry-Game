using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites = new();
    [Space]
    [SerializeField] float minRotation = 10f;
    [SerializeField] float maxRotation = 30f;
    [Space]
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 1f;
    [Space]
    [SerializeField] Vector3 start;
    [SerializeField] Vector3 end;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float lifetime = 10f;
    [Space]
    [SerializeField] Vector3 direction = Vector3.down;
    [SerializeField] Color startColor = Color.white;
    [SerializeField] float spawnScale = 1f;

    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeBetweenSpawns)
        {
            timer -= timeBetweenSpawns;
            SpawnSprite();
        }
    }

    void SpawnSprite()
    {
        Vector3 spawnPoint = RandomPoint();
        var inScene = new GameObject("Sprite");
        inScene.transform.eulerAngles = new Vector3(0, 0, Random.Range(0f, 360f));
        inScene.transform.position = spawnPoint;
        inScene.transform.localScale = spawnScale * Vector3.one;

        var sr = inScene.AddComponent<SpriteRenderer>();
        sr.sprite = RandomSprite();
        sr.color = startColor;

        var mover = inScene.AddComponent<Mover>();
        mover.speed = Random.Range(minSpeed, maxSpeed);
        mover.rotationalSpeed = Random.Range(minRotation, maxRotation) * (Random.Range(0f, 1f) > 0.5f ? 1 : -1);
        mover.direction = direction;

        Destroy(inScene, lifetime);
    }

    Vector3 RandomPoint()
    {
        return new Vector3()
        {
            x = Random.Range(start.x, end.x),
            y = Random.Range(start.y, end.y),
            z = Random.Range(start.z, end.z),
        };
    }

    Sprite RandomSprite()
    {
        return sprites[Random.Range(0, sprites.Count)];
    }

    private void OnDrawGizmos()
    {
        var corners = new Vector3[8];
        corners[0] = new Vector3(start.x, start.y, start.z);
        corners[1] = new Vector3(end.x, start.y, start.z);
        corners[2] = new Vector3(end.x, end.y, start.z);
        corners[3] = new Vector3(start.x, end.y, start.z);
        corners[4] = new Vector3(start.x, start.y, end.z);
        corners[5] = new Vector3(end.x, start.y, end.z);
        corners[6] = new Vector3(end.x, end.y, end.z);
        corners[7] = new Vector3(start.x, end.y, end.z);

        Debug.DrawLine(corners[0], corners[1]);
        Debug.DrawLine(corners[1], corners[2]);
        Debug.DrawLine(corners[2], corners[3]);
        Debug.DrawLine(corners[3], corners[0]);
        Debug.DrawLine(corners[4], corners[5]);
        Debug.DrawLine(corners[5], corners[6]);
        Debug.DrawLine(corners[6], corners[7]);
        Debug.DrawLine(corners[7], corners[4]);
        Debug.DrawLine(corners[0], corners[4]);
        Debug.DrawLine(corners[1], corners[5]);
        Debug.DrawLine(corners[2], corners[6]);
        Debug.DrawLine(corners[3], corners[7]);

        var origin = (start + end) / 2;
        Debug.DrawLine(origin, origin + direction);
    }
}
