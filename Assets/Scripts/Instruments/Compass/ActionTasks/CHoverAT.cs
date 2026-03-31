using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.InputSystem;


namespace NodeCanvas.Tasks.Actions {

	public class CHoverAT : ActionTask {

		public float compassSpan = 1f;

		Compass compass;

		protected override string OnInit() {

            compass = blackboard.GetVariable<Compass>("compass").value;
            if (!compass)
                return $"{agent.name} requires a Compass component!";

            return null;
		}

		protected override void OnUpdate() {
			compass.Hover();
			Anchor anchor = AnchorManager.GetHoveredAnchor();
			if (anchor && Mouse.current.leftButton.wasPressedThisFrame && !GameUtils.CursorOverUI)
			{
				compass.Measure(anchor.transform.position);
				EndAction(true);
			}
		}
	}
}