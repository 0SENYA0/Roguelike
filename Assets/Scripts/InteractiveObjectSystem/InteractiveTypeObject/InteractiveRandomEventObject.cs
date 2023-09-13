using System.Collections.Generic;
using Assets.Enemy;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class InteractiveRandomEventObject : InteractiveObject
    {
        [SerializeField] private InteractiveLootObject _interactiveLootObject;
        [SerializeField] private List<EnemyView> _enemyView;
        
        public InteractiveLootObject RandomLoot => _interactiveLootObject;
        public IEnemyPresenter GetRandomEnemy() => new EnemyPresenter(_enemyView[Random.Range(0, _enemyView.Count)]);
    }
}