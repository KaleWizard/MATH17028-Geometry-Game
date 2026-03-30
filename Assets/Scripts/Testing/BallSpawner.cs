using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    float timer = 0;
    float time = 2;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > time)
        {
            timer -= time;
            Instantiate(ballPrefab, transform.position, Quaternion.identity);
        }
    }
}
