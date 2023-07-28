using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Person
{
    public class Weapon
    {
        public AttackData AttackData;
        
        public int ChanceToSplash { get; set; }
        public int MinValueToCriticalDamage { get; set; }
        public int ValueModifier { get; set; }
    }

    public struct SplashAttack : IAttack
    {
        private readonly AttackData _attackData;
        private readonly IDamagable _target;

        public SplashAttack(AttackData attackData, IDamagable target)
        {
            _attackData = attackData;
            _target = target;
        }

        public void Attack() =>
            _target.TakeDamage(_attackData.DamageData);
    }

    public struct AOEAttack : IAttack
    {
        private readonly AttackData _attackData;
        private readonly List<IDamagable> _targets;

        public AOEAttack(AttackData attackData, List<IDamagable> targets)
        {
            _attackData = attackData;
            _targets = targets;
        }

        public void Attack()
        {
            foreach (IDamagable target in _targets)
                target.TakeDamage(_attackData.DamageData);
        }
    }

    public static class AttackManager
    {
        public static IAttack GetAttack<TTarget>(TTarget target, AttackData attackData) where TTarget : IDamagable
        {
            switch (attackData.AttackType)
            {
                // case AttackType.Splash:
                //     return attackData is SplashAttackData splashAttackData ? new SplashAttack(attackData, GetRandomTarget(target)) : null;
                //     break;
                // case AttackType.AOE:
                //     return attackData is AOEAttackData aoeAttackData ? new AOEAttack(attackData, target) : null;
                //     break;
            }

            return null;
        }

        private static IDamagable GetRandomTarget(List<IDamagable> target) =>
            target[Random.Range(0, target.Count)];
    }

    public class SplashAttackData : AttackData
    {

    }

    public class AOEAttackData : AttackData
    {

    }

    public class AttackData
    {
        public DamageData DamageData;
        public AttackType AttackType;
    }

    public enum AttackType
    {
        AOE,
        Splash
    }
}