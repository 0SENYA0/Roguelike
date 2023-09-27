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
            float damage = 0;

            if (_inventoryPresenter.ActiveArmor == null)
            {
                damage = weapon.Damage * 2;
            }
            else
            {
                damage = CalculateNewDamage(weapon);
            }

            _health -= damage;

            if (IsDie)
                return;
        }

        private float CalculateNewDamage(IWeapon weapon)
        {
            float elementMultiplier = CalculateDamageModifier(weapon.Element, _armor.Body.Element);
            float damage = elementMultiplier * weapon.Damage;
            float armorValue =
                (_inventoryPresenter.ActiveArmor.Body.Value + _inventoryPresenter.ActiveArmor.Head.Value);

            return damage - armorValue;
        }
    }
}