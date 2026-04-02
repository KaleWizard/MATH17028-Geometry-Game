using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ParadoxNotion.Services;
using UnityEngine;
using UnityEngine.InputSystem;


namespace NodeCanvas.Tasks.Actions {

	public class SEDrawAT : ActionTask
    {
        public BBParameter<GameObject> drawingObj;

        Line linePrefab;

		Line inScene;

        StraightEdge se;

        Vector2 anchor;
        Vector2 direction;

        float min;
        float max;
        Vector3 startPosition;

        bool drawing;

        protected override string OnInit()
        {
            se = blackboard.GetVariable<StraightEdge>("straightEdge").value;
            if (!se)
                return $"{agent.name} requires a StraightEdge component!";

			linePrefab = Resources.Load<Line>("Prefabs/Line");

            return null;
        }

        protected override void OnExecute() {
            inScene = MonoManager.Instantiate(linePrefab);
            drawingObj.value = inScene.gameObject;
            anchor = se.AnchorA;
            direction = (Vector2) se.AnchorB - anchor;
            direction.Normalize();

            drawing = false;

            inScene.SetRotation(direction);
		}

		protected override void OnUpdate() {
			if (!drawing)
            {
                HoverUpdate();
                if (Mouse.current.leftButton.wasPressedThisFrame && !GameUtils.CursorOverUI)
                {
                    drawing = true;
                }
            }
            else
            {
                DrawUpdate();
                if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    inScene.GenerateColliders();
                    drawingObj.value = null;

                    DrawableManager.Instance.AddDrawable(inScene, true);

                    EndAction(true);
                }
            }
		}

        void HoverUpdate()
        {
            (min, startPosition) = GetPointOnLine();
            max = min;
            inScene.SetPositionLength(startPosition, 0);
        }

        void DrawUpdate()
        {
            var (dist, pos) = GetPointOnLine();

            if (dist < min)
            {
                min = dist;
                startPosition = pos;
            } else if (dist > max)
            {
                max = dist;
            }

            inScene.SetPositionLength(startPosition, max - min);

            DrawableManager.Instance.TryAddAnchors(inScene);
        }

        (float, Vector3) GetPointOnLine()
        {
            Vector3 mouseRelative = GameUtils.WorldMousePosition() - anchor;

            float distance = Vector2.Dot(mouseRelative, direction);
            distance = Mathf.Clamp(distance, 0, se.Length);
            
            return (distance, anchor + direction * distance);
        }
	}
}