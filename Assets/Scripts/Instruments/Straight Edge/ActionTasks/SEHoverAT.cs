using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.InputSystem;


namespace NodeCanvas.Tasks.Actions {

	public class SEHoverAT : ActionTask
    {

        StraightEdge se;

        protected override string OnInit()
        {
            se = blackboard.GetVariable<StraightEdge>("straightEdge").value;
            if (!se)
                return $"{agent.name} requires a StraightEdge component!";

            return null;
        }

        protected override void OnUpdate() {
			Anchor hovered = AnchorManager.GetHoveredAnchor();
			if (hovered && Mouse.current.leftButton.wasPressedThisFrame)
			{
				se.SetAnchor(hovered.transform.position);
				EndAction(true);
			}
		}
	}
}