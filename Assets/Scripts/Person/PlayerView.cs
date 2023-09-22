using Assets.Infrastructure;
using Assets.Inventory;
using Assets.Player;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Person
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private SpriteAnimation _spriteAnimation;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _inventorySize = 10;
        
        private PlayerPresenter _playerPresenter;
        private InventoryPresenter _inventoryPresenter;

        private void Start()
        {
            _inventoryPresenter =  new InventoryPresenter(_inventorySize);
            _playerPresenter = new PlayerPresenter(this, _inventoryPresenter);
        }

        public InventoryPresenter InventoryPresenter => _inventoryPresenter;
        public IPlayerPresenter PlayerPresenter => _playerPresenter;
        public int Health => _health;
        public SpriteAnimation SpriteAnimation => _spriteAnimation;
        public Sprite Sprite => _sprite;
    }
}