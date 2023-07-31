using Assets.Person;

namespace Assets.Weapon
{
    public class Weapon
    {
        public AttackData AttackData;
        
        public int ChanceToSplash { get; set; }
        public int MinValueToCriticalDamage { get; set; }
        public int ValueModifier { get; set; }
    }

    public class AttackData
    {
        public DamageData DamageData;
    }
}