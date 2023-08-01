using Assets.Interface;
using UnityEngine;

namespace Assets.Weapon
{
    public class Weapon : IWeapon
    {
        public Weapon(DamageData damageData, int chanceToSplash, int minValueToCriticalDamage, int valueModifier, ParticleSystem particleSystem)
        {
            DamageData = damageData;
            ChanceToSplash = chanceToSplash;
            MinValueToCriticalDamage = minValueToCriticalDamage;
            ValueModifier = valueModifier;
            ParticleSystem = particleSystem;
        }

        public ParticleSystem ParticleSystem { get; }
        public DamageData DamageData { get; }
        public int ChanceToSplash { get; }
        public int MinValueToCriticalDamage { get; }
        public int ValueModifier { get; }
    }
}