using Assets.Person;

namespace Assets.Weapon
{
    public class Weapon
    {
        public DamageData DamageData;
        
        public int ChanceToSplash { get; set; }
        public int MinValueToCriticalDamage { get; set; }
        public int ValueModifier { get; set; }
    }
}