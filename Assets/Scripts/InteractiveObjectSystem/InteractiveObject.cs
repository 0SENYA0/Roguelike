using System;
using Assets.Fight.Element;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.InteractiveObjectSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private ObjectType _type;
        [SerializeField] private string _name;
        [Multiline]
        [SerializeField] private string _data;

        public ObjectType Type => _type;

        private Element _element;

        private void Start()
        {
            _element = GetRandomElement();
        }

        public void DestroyObject()
        {
            Destroy(this.gameObject);
        }

        public InteractiveObjectData GetData()
        {
            return new InteractiveObjectData(_name, _data, _element);
        }

        public GameObject GetObject()
        {
            return this.gameObject;
        }

        private Element GetRandomElement()
        {
            return (Element) Random.Range(0, 5);
        }
    }
}