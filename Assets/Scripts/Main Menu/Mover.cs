using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public float rotationalSpeed;

    void Update()
    {
        transform.position += Time.deltaTime * speed * direction;

        var rotation = transform.eulerAngles;
        rotation.z += rotationalSpeed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }
}
