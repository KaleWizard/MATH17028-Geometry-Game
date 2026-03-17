using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class SEResetAT : ActionTask {

		StraightEdge se;

		protected override string OnInit() {
			se = blackboard.GetVariable<StraightEdge>("straightEdge").value;
			if (!se)
				return $"{agent.name} requires a StraightEdge component!";

			return null;
		}

		protected override void OnExecute() {
			se.ResetAnchors();
			EndAction(true);
		}
	}
}