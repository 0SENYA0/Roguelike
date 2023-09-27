using Assets.DefendItems;
using Assets.Interface;
using Assets.Person;
using Assets.Scripts.AnimationComponent;

namespace Assets.Enemy
{
    public class Enemy : Unit
    {
        private string _name;
        private string _data;

        public Enemy(int health, IWeapon weapon, Armor armor,  SpriteAnimation spriteAnimation) 
            : base(health, weapon, armor, spriteAnimation)
        {
        }
        
        public bool IsBoss { get; private set; }

        public void MakeBoss() =>
            IsBoss = true;
    }
}