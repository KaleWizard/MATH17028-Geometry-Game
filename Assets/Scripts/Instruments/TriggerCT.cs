using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class TriggerCT : ConditionTask {

		public BBParameter<bool> triggerBBP;

		protected override bool OnCheck() {
			bool result = triggerBBP.value;
			triggerBBP.value = false;
			return result;
		}
	}
}