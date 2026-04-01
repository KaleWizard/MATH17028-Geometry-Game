using UnityEngine;

public class DestroyInSeconds : MonoBehaviour
{
    [SerializeField] float seconds = 15f;

    void Start()
    {
        Destroy(gameObject, seconds);
    }
}
