using System;
using Assets.DefendItems;
using Assets.Fight.Element;
using Assets.Interface;
using Assets.Inventory;
using Assets.Person;
using Assets.Scripts.AnimationComponent;

namespace Assets.Player
{
    public class Player : Unit
    {
        private readonly InventoryPresenter _inventoryPresenter;

        public InventoryPresenter InventoryPresenter => _inventoryPresenter;

        private readonly IWeapon _weapon;
        private readonly Armor _armor;
        private readonly MagicItem _magicItem;

        public Player(float health,
            InventoryPresenter inventoryPresenter,
            SpriteAnimation spriteAnimation)
            : base(health, null, null, null, spriteAnimation)
        {
            _health = health;
            _inventoryPresenter = inventoryPresenter;
        }

        protected override void CalculateDamageMultiplier(IWeapon weapon)
        {
            _health -= Convert.ToInt32(weapon.Damage);
 
            if (IsDie)
            {
                return;
            }

            if (_inventoryPresenter.ActiveArmor == null)
            {
                _health -= weapon.Damage * 2;
                return;
            }

            float damageMultiplier = CalculateNewDamage(weapon);
            _health -= damageMultiplier * weapon.Damage;
        }
        
        protected override float CalculateDamageModifier(Element element) =>
            ElementManager.GetDamageModifier(element, _inventoryPresenter.ActiveArmor.Body.Element);

        private float CalculateNewDamage(IWeapon weapon)
        {
            float damage = weapon.Damage;
            float element = CalculateDamageModifier(weapon.Element);
            float body = _inventoryPresenter.ActiveArmor.Body.Value;
            float head = _inventoryPresenter.ActiveArmor.Head.Value;
            float armor = body + head;
            
            return damage / (element * damage - armor);
        }
    }
}