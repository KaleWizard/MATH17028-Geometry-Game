using System.Collections.Generic;
using UnityEngine;

public class LevelClearManager : MonoBehaviour
{
    [SerializeField] float timeToClear = 1f;
    [SerializeField] BoxCollider2D ballTrigger;
    [Space]
    [SerializeField] List<GameObject> toEnable;

    bool simulating = false;

    Transform ball;

    float timer = 0;

    public void ActiveBall(GameObject ball)
    {
        if (simulating)
        {
            Destroy(ball);
            return;
        }

        simulating = true;

        this.ball = ball.transform;
    }

    void Update()
    {
        if (!simulating) return;

        if (ballTrigger.OverlapPoint(ball.position))
        {
            timer += Time.deltaTime;
        } else
        {
            timer = 0;
        }

        if (timer > timeToClear)
        {
            OnLevelCleared();
        }
    }

    void OnLevelCleared()
    {
        simulating = false;
        foreach (GameObject obj in toEnable)
            obj.SetActive(true);
    }
}
