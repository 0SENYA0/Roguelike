using System;
using Assets.DefendItems;
using Assets.Interface;
using UnityEngine;

namespace Assets.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObject/PlayerInventory", order = 0)]
    public class PlayerInventoryScriptableObject : ScriptableObject
    {
        [SerializeField] private ArmorScriptableObject[] _armors;
        [SerializeField] private WeaponScriptableObject[] _weapons;

        private MagicItem[] _magicItems;

        public MagicItem[] MagicItems => _magicItems;

        private Armor[] Armors;
        private Weapon.Weapon[] Weapons;

        public Weapon.Weapon[] GetWeapons()
        {
            Weapons = new Weapon.Weapon[5];
            
            for (int i = 0; i < _weapons.Length; i++)
            {
                Weapons[i] = new Weapon.Weapon(
                    _weapons[i].Damage, _weapons[i].Element, _weapons[i].ChanceToSplash, _weapons[i].MinValueToCriticalDamage,
                    _weapons[i].ValueModifier, _weapons[i].ParticleSystem) ;
            }

            return Weapons;
        }
        
        public Armor[] GetArmors()
        {
            Armors = new Armor[5];

            for (int i = 0; i < _armors.Length; i++)
            {
                Armors[i] = new Armor(
                    new Body(_armors[i].BodyPart.Value, _armors[i].BodyPart.Element),
                    new Head (_armors[i].HeadPart.Value), 
                    _armors[i].ParticleSystem);
            }

            return Armors;
        }
    }
}