using UnityEngine;

namespace Assets.Scripts.SoundSystem
{
    [System.Serializable]
    public class AudioItem
    {
        [SerializeField] private CollectionOfSounds _type;
        [SerializeField] private AudioClip _clip;
        [SerializeField] [Range(0, 1)] private float _volume;

        public CollectionOfSounds Type => _type;
        public AudioClip Clip => _clip;
        public float Volume => _volume;
    }
}