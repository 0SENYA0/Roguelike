using UnityEngine;

namespace Assets.Interface
{
	public interface IStepFightView
	{
		void SetSprite(Sprite sprite, int index);

		void ActiveFrame(int index);

		void Hide();

		void Show();
	}
}