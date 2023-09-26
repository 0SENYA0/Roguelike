using Assets.Inventory;
using Assets.Player;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Person
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _health = 800;
        [SerializeField] private SpriteAnimation _spriteAnimation;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _inventorySize = 10;
        
        private PlayerPresenter _playerPresenter;
        private InventoryPresenter _inventoryPresenter;
        
        public void Init()
        {
            _inventoryPresenter =  new InventoryPresenter(_inventorySize);
            _playerPresenter = new PlayerPresenter(this, _inventoryPresenter);
        }

        public void Init(float health, InventoryPresenter inventory)
        {
            _health = health;
            _inventoryPresenter = inventory;
            _playerPresenter = new PlayerPresenter(this, _inventoryPresenter);
        }

        public InventoryPresenter InventoryPresenter => _inventoryPresenter;
        public IPlayerPresenter PlayerPresenter => _playerPresenter;
        public float Health => _health;
        public SpriteAnimation SpriteAnimation => _spriteAnimation;
        public Sprite Sprite => _sprite;
    }
}