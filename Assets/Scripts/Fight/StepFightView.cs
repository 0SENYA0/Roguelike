using System.Collections.Generic;
using Assets.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class StepFightView : MonoBehaviour, IStepFightView
    {
        [SerializeField] private List<Image> _image;

        public void SetSprite(Sprite sprite, int index) =>
            _image[index].sprite = sprite;

        public void Hide()
        {
            foreach (Image image in _image)
            {
                image.gameObject.SetActive(false);
            }
        }

        public void Show()
        {
            foreach (Image image in _image)
            {
                image.gameObject.SetActive(true);
            }
        }
    }
}