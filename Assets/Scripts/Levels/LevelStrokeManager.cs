using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using TMPro;
using UnityEngine;

public class LevelStrokeManager : MonoBehaviour
{
    [SerializeField] int maxStrokes = 3;
    [Space]
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Blackboard controllerBB;

    int strokesLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        strokesLeft = maxStrokes;
        DrawableManager.Instance.OnNewDrawable.AddListener(DrawableAdded);
    }

    void DrawableAdded(int total)
    {
        strokesLeft = maxStrokes - total;
        UpdateText();
        if (strokesLeft <= 0)
            controllerBB.SetVariableValue("noStrokesLeft", true);
    }

    private void OnValidate()
    {
        if (maxStrokes <= 0)
            maxStrokes = 1;
        strokesLeft = maxStrokes;
        UpdateText();
    }

    void UpdateText()
    {
        text.text = $"{strokesLeft}/{maxStrokes} Lines left";
    }
}
