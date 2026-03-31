using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class StopFSMAT : ActionTask {

		protected override void OnExecute() {
			var fsm = agent.GetComponent<FSMOwner>();
			fsm.StopBehaviour();
			EndAction(true);
		}
	}
}