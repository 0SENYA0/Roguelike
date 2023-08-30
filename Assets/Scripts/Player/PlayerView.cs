using Assets.ScriptableObjects;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Player
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public int Health { get; private set; }
        [field: SerializeField] public ArmorScriptableObject ArmorScriptableObject { get; private set; }
        [field: SerializeField] public WeaponScriptableObject WeaponScriptableObject { get; private set; }
        [field: SerializeField] public SpriteAnimation SpriteAnimation { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }

        private PlayerPresenter _playerPresenter;
        public PlayerPresenter PlayerPresenter => _playerPresenter;

        private void Start() =>
            _playerPresenter = new PlayerPresenter(this);
    }
}