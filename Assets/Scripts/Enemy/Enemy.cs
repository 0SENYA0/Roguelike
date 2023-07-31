using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;

namespace Assets.Enemy
{
    public class Enemy : Person.Person, IInteractiveObject
    {
        [SerializeField] private string _name;
        [Multiline]
        [SerializeField] private string _data;
        
        public bool Boss { get; private set; }

        // Временный метод, чтобы удалять противников с поля
        public void DestroyObject()
        {
            Destroy(gameObject);
        }

        public InteractiveObjectData GetData()
        {
            return new InteractiveObjectData(_name, _data);
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }
    }
}