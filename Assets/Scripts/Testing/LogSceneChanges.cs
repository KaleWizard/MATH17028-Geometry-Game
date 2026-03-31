using UnityEngine;
using UnityEngine.SceneManagement;

public class LogSceneChanges : SingletonBehaviour<LogSceneChanges>
{
    void Start()
    {
        SceneManager.activeSceneChanged += (_, _) => Debug.Log("ACTIVE SCENE CHANGED");
        SceneManager.sceneUnloaded += (_) => Debug.Log("UNLOADED");
        SceneManager.sceneLoaded += (_, _) => Debug.Log("LOADED");
    }
}
