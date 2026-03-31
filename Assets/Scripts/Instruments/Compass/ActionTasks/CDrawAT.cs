using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;
using UnityEngine.InputSystem;


namespace NodeCanvas.Tasks.Actions {

	public class CDrawAT : ActionTask
    {
        public BBParameter<float> radiusBBP;

        Compass compass;

        Arc arcPrefab;
        Arc inScene;

        bool drawing;

        float min;
        float max;

        Vector2 direction;

        protected override string OnInit()
        {

            compass = blackboard.GetVariable<Compass>("compass").value;
            if (!compass)
                return $"{agent.name} requires a Compass component!";

            arcPrefab = Resources.Load<Arc>("Prefabs/Arc");

            return null;
        }

        protected override void OnExecute()
        {
            drawing = false;
		}

		protected override void OnUpdate()
        {
            direction = compass.DrawingPosition(compass.transform.position, radiusBBP.value);
            if (!drawing)
            {
                if (Mouse.current.leftButton.wasPressedThisFrame && !GameUtils.CursorOverUI)
                {
                    drawing = true;

                    inScene = Object.Instantiate(arcPrefab);
                    inScene.transform.right = direction;
                    inScene.SetRadius(radiusBBP.value);
                    inScene.SetArcLength(0, 0);
                    inScene.SetPosition(compass.transform.position);

                    min = max = Vector2.SignedAngle(Vector2.left, direction) + 180f;
                }
            }
            else
            {
                DrawUpdate();
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    inScene.GenerateColliders();
                    DrawableManager.Instance.AddDrawable(inScene);

                    EndAction(true);
                }
            }
        }

        void DrawUpdate()
        {
            float angle = Vector2.SignedAngle(Vector2.left, direction) + 180f;

            float toMin = (angle > max ? angle - 360f : angle) - min;
            float toMax = (angle < min ? angle + 360f : angle) - max;

            if (toMin > 0 && toMax < 0) return;

            if (toMin < 0 && Mathf.Abs(toMin) < Mathf.Abs(toMax))
            {
                min += toMin;

                inScene.SetArcLength(min * Mathf.Deg2Rad, max * Mathf.Deg2Rad);
                inScene.SetRotation(direction);
            } 
            else if (toMax > 0 && Mathf.Abs(toMin) > Mathf.Abs(toMax))
            {
                max += toMax;
                inScene.SetArcLength(min * Mathf.Deg2Rad, max * Mathf.Deg2Rad);
            }

            DrawableManager.Instance.TryAddAnchors(inScene);
        }
	}
}