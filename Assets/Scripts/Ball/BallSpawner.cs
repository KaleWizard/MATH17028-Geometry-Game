using NodeCanvas.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject ballprefab;
    [SerializeField] Blackboard instrumentBlackboard;
    [SerializeField] List<GameObject> toDisable;
    [SerializeField] LevelClearManager levelManager;

    public void OnDropBall()
    {
        var inScene = Instantiate(ballprefab, spawnPoint.position, Quaternion.identity);
        levelManager.ActiveBall(inScene);

        instrumentBlackboard.SetVariableValue("gameSimulating", true);
        foreach (var obj in toDisable)
            obj.SetActive(false);
    }
}
