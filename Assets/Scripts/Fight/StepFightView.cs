using System.Collections.Generic;
using Assets.Interface;
using Assets.Scripts.AnimationComponent;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
	public class StepFightView : MonoBehaviour, IStepFightView
	{
		[SerializeField] private List<Image> _image;
		[SerializeField] private List<UISpriteAnimation> _targetFrames;

		public void SetSprite(Sprite sprite, int index) => 
			_image[index].sprite = sprite;

		public void ActiveFrame(int index)
		{
			ChangeObjectsActive(_targetFrames, false);

			if (index >= _targetFrames.Count)
				return;

			_targetFrames[index].gameObject.SetActive(true);
		}

		public void Hide()
		{
			ChangeObjectsActive(_image, false);
			ChangeObjectsActive(_targetFrames, false);
		}

		public void Show()
		{
			ChangeObjectsActive(_image, true);
			ChangeObjectsActive(_targetFrames, false);
		}

		private void ChangeObjectsActive(List<Image> collection, bool active)
		{
			foreach (var item in collection)
				item.gameObject.SetActive(active);
		}

		private void ChangeObjectsActive(List<UISpriteAnimation> collection, bool active)
		{
			foreach (var item in collection)
				item.gameObject.SetActive(active);
		}
	}
}