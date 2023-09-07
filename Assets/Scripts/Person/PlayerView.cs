using System;
using Assets.Player;
using Assets.ScriptableObjects;
using Assets.Scripts.AnimationComponent;
using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Person
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private int _health;
        // [SerializeField] private ArmorScriptableObject _armorScriptableObject;
        // [SerializeField] private WeaponScriptableObject _weaponScriptableObject;
        [SerializeField] private SpriteAnimation _spriteAnimation;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private PlayerInventoryScriptableObject _playerInventory;
        
        private PlayerPresenter _playerPresenter;

        private void Start() => 
            _playerPresenter = new PlayerPresenter(this);

        public PlayerInventoryScriptableObject PlayerInventory => _playerInventory;
        public IPlayerPresenter PlayerPresenter => _playerPresenter;

        public int Health => _health;

        public SpriteAnimation SpriteAnimation => _spriteAnimation;

        public Sprite Sprite => _sprite;
    }
}