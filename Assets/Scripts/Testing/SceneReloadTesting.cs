using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneReloadTesting : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("AWAKE");
    }

    void Start()
    {
        Debug.Log("STARTED");
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("DESTROYED");
    }
}
