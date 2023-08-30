using System;
using Assets.Enemy;
using Assets.ScriptableObjects;
using Assets.Scripts.AnimationComponent;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class EnemyView : InteractiveObject, IEnemyObjectData
    {
        [field: SerializeField] public int Health { get; private set;}
        [field: SerializeField] public WeaponScriptableObject Weapon { get; private set;}
        [field: SerializeField] public ArmorScriptableObject Armor { get; private set;}
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public SpriteAnimation SpriteAnimation { get;private set; }


        // private int _count;
        // private string data = "количество врагов = ";
        // private MagicItem _magicItem;
        //
        // public List<Enemy.Enemy> Enemy { get; private set; }

        private EnemyPresenter _enemyPresenter;
        public string Name => _name;
        public EnemyPresenter EnemyPresenter => _enemyPresenter;
        protected override void OnStart()
        {
            _enemyPresenter = new EnemyPresenter(this);

            #region MyRegion

            //
            //
            //
            // Enemy = new List<Enemy.Enemy>();
            // _count = GenerateRandomCountEnemy();
            // AddInfoInData(data + _count);
            // _weapon.SetNewElement(GetRandomElement());
            // Armor.BodyPart.SetNewElement(GetRandomElement());
            //
            // for (int i = 0; i < _count; i++)
            // {
            //     Enemy.Enemy enemy = GetEnemy();
            //     enemy.Sprite = Sprite;
            //
            //     if (Type == ObjectType.Boos)
            //         enemy.MakeBoss();
            //
            //     Enemy.Add(enemy);
            // }

            #endregion
        }

        // private Enemy.Enemy GetEnemy()
        // {
        //     EnemyFactory factory = new EnemyFactory();
        //     WeaponFactory weaponFactory = new WeaponFactory();
        //     ArmorFactory armorFactory = new ArmorFactory();
        //
        //     IWeapon weapon = weaponFactory.Create(_weapon.Damage, _weapon.Element, _weapon.ChanceToSplash, _weapon.MinValueToCriticalDamage,
        //         _weapon.ValueModifier, _weapon.ParticleSystem);
        //
        //     Body body = new Body(Armor.BodyPart.Value, Armor.BodyPart.Element);
        //     Head head = new Head(Armor.HeadPart.Value);
        //
        //     Armor armor = armorFactory.Create(body, head, Armor.ParticleSystem);
        //
        //     return factory.Create(weapon, armor, _health, SpriteAnimation);
        // }

        // private int GenerateRandomCountEnemy() =>
        //     Random.Range(1, 4);
    }

    public interface IEnemyObjectData : IInteractiveObjectData
    {
        public EnemyPresenter EnemyPresenter { get; }
    }

    public interface IInteractiveObjectData
    {
        public string Name { get; }
        public string Data { get; }
    }
}