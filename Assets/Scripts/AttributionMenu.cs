using UnityEngine;

public class AttributionMenu : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    void Start()
    {
        Disable();
    }

    public void Enable()
    {
        canvas.SetActive(true);
    }

    public void Disable()
    {
        canvas.SetActive(false);
    }
}
