using TMPro;
using UnityEngine;

namespace Assets.UI
{
    public class Version : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _text.text = $"version: {Application.version}";
        }
    }
}