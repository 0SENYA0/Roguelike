using Assets.Weapon;
using UnityEngine;

namespace Assets.Interface
{
    public interface IWeapon
    {
        ParticleSystem ParticleSystem { get; }
        DamageData DamageData { get; }
        int ChanceToSplash { get; }
        int MinValueToCriticalDamage { get; }
        int ValueModifier { get; }
    }
}