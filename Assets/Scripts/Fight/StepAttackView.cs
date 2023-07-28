using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Fight
{
    public class StepAttackView : MonoBehaviour
    {
        [SerializeField] private List<Image> _image;

        public void SetSprite(Sprite sprite, int index) =>
            _image[index].sprite = sprite;
    }
}