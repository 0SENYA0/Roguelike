using Assets.Person;
using UnityEngine;

namespace Assets.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField] public Sprite Sprite { get; }
        public DamageData DamageData { get; set; }
        public int ChanceToSplash { get; set; }
        public int MinValueToCriticalDamage { get; set; }
        public int ValueModifier { get; set; }
    }
}