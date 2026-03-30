using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class HoveringUICT : ConditionTask {

		protected override bool OnCheck() {
			return GameUtils.CursorOverUI;
		}
	}
}