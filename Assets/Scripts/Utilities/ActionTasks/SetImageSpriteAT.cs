using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.UI;


namespace NodeCanvas.Tasks.Actions {

	public class SetImageSpriteAT : ActionTask {

		public BBParameter<Image> image;
		public Sprite sprite;

		protected override void OnExecute() {
			image.value.sprite = sprite;
			EndAction(true);
		}
	}
}