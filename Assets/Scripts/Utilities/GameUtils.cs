using UnityEngine;
using UnityEngine.InputSystem;

public class GameUtils
{
    public static Vector2 WorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
    }
}
