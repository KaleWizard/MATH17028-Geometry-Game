using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.InputSystem;


namespace NodeCanvas.Tasks.Actions {

	public class COneAnchorAT : ActionTask {

        public BBParameter<float> radiusBBP;

        Compass compass;

        protected override string OnInit()
        {

            compass = blackboard.GetVariable<Compass>("compass").value;
            if (!compass)
                return $"{agent.name} requires a Compass component!";

            return null;
        }

        protected override void OnExecute() {
			
		}

		protected override void OnUpdate() {
			compass.Measure(compass.transform.position);
            Anchor anchor = AnchorManager.GetHoveredAnchor();
            if (anchor 
                && compass.IsValidAnchor(anchor.transform.position) 
                && Mouse.current.leftButton.wasPressedThisFrame
                && !GameUtils.CursorOverUI)
            {
                radiusBBP.value = Vector3.Distance(anchor.transform.position, compass.transform.position);
                EndAction(true);
            }
		}
	}
}