using UnityEngine;
using UnityEngine.InputSystem;

public class QuitSingleton : SingletonBehaviour<QuitSingleton>
{
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            Application.Quit();
    }
}
