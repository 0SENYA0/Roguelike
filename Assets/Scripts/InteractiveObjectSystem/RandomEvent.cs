using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class RandomEvent : MonoBehaviour, IInteractiveObject
    {
        [SerializeField] private string _name;
        [Multiline]
        [SerializeField] private string _data;

        public ObjectType ObjectType { get; set; }

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