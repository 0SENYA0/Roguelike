using Assets.Scripts.SoundSystem;
using TMPro;
using UnityEngine;

namespace Assets.TeachingLevels
{
    public class TeachingAreaText : MonoBehaviour
    {
        [SerializeField] private SoundComponent _soundComponent;
        [SerializeField] private TMP_Text _text;

        public SoundComponent SoundComponent => _soundComponent;
        public TMP_Text Text => _text;
    }
}