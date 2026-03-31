using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class CMeasuredOneAnchorAT : ActionTask
    {
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
            compass.DrawingPosition(compass.transform.position, radiusBBP.value);

		}
	}
}