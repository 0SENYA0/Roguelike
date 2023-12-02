using System.Collections.Generic;
using Assets.Enemy;
using Assets.Inventory;
using Assets.Inventory.ItemGeneratorSystem;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveRandomEventObject : InteractiveObject
    {
        [SerializeField] private List<EnemyView> _enemyView;

        public IEnemyPresenter GetRandomEnemy() => new EnemyPresenter(_enemyView[Random.Range(0, _enemyView.Count)]);

        public IInventoryItem GetRandomLoot()
        {
            if (Random.Range(0, 2) == 0)
                return ItemGenerator.Instance.GetRandomWeapon();

            return ItemGenerator.Instance.GetRandomArmor();
        }
    }
}