using UnityEngine;

namespace Assets.Interface
{
    public interface IStepFightView
    {
        public void SetSprite(Sprite sprite, int index);

        public void Hide();
        public void Show();

    }
}