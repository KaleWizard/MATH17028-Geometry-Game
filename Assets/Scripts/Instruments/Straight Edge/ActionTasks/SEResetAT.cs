using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SEResetAT : ActionTask {

		public BBParameter<GameObject> drawing;
		StraightEdge se;

		protected override string OnInit() {
			se = blackboard.GetVariable<StraightEdge>("straightEdge").value;
			if (!se)
				return $"{agent.name} requires a StraightEdge component!";

			return null;
		}

		protected override void OnExecute() {
			if (drawing.value != null)
			{
				MonoManager.Destroy(drawing.value);
				drawing.value = null;
			}

			se.ResetAnchors();
			EndAction(true);
		}
	}
}