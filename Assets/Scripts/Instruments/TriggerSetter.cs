using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class TriggerSetter : MonoBehaviour
{
    Variable<bool> compassTrigger;
    Variable<bool> straightEdgeTrigger;

    void Start()
    {
        var blackboard = GetComponent<Blackboard>();

        compassTrigger = blackboard.GetVariable<bool>("compassTrigger");
        straightEdgeTrigger = blackboard.GetVariable<bool>("straightEdgeTrigger");
    }

    public void SetCompassTrigger()
    {
        compassTrigger.value = true;
    }

    public void SetStraightEdgeTrigger()
    {
        straightEdgeTrigger.value = true;
    }
}
