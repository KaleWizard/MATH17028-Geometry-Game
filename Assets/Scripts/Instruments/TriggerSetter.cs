using NodeCanvas.Framework;
using UnityEngine;

public class TriggerSetter : MonoBehaviour
{
    Variable<bool> compassTrigger;
    Variable<bool> straightEdgeTrigger;

    void Start()
    {
        var blackboard = GetComponentInChildren<Blackboard>();

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
