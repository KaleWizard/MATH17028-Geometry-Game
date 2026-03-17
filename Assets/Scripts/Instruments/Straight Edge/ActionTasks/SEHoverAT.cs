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

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute() {
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Anchor hovered = AnchorManager.GetHoveredAnchor();
			if (hovered && Mouse.current.leftButton.wasPressedThisFrame)
			{
				se.SetAnchor(hovered.transform.position);
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}