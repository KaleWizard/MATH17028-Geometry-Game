using UnityEngine;
using UnityEngine.InputSystem;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Instantiate(ballPrefab, GameUtils.WorldMousePosition(), Quaternion.identity);
        }
    }
}
