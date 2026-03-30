using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SetActiveAT : ActionTask {

		public BBParameter<bool> activeBBP;
		public BBParameter<GameObject> gameObjectBBP;

		protected override void OnExecute() {
			gameObjectBBP.value.SetActive(activeBBP.value);
			EndAction(true);
		}
	}
}