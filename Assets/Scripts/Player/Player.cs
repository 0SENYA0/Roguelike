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
                return;

            float damageMultiplier = weapon.Damage /
                                     (this.CalculateDamageModifier(weapon.Element)
                                      * weapon.Damage
                                      + (_inventoryPresenter.ActiveArmor.Body.Value +
                                         _inventoryPresenter.ActiveArmor.Head.Value)
                                     );
            _health -= damageMultiplier * weapon.Damage;
        }
        
        protected override float CalculateDamageModifier(Element element) =>
            ElementManager.GetDamageModifier(element, _inventoryPresenter.ActiveArmor.Body.Element);
    }
}