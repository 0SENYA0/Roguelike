using Assets.Fight.Element;
using Assets.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.InteractiveObjectSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] protected LocalizedText _translationName;
        [SerializeField] private ObjectType _type;
        [SerializeField] private int _numberOfAwards = 1;

        private Element _element;
        
        public ObjectType Type => _type;
        public int NumberOfAwards => _numberOfAwards;
        
        private void Start()
        {
            _element = GetRandomElement();
            OnStart();
        }

        public void DestroyObject()
        {
            Destroy(this.gameObject);
        }

        public InteractiveObjectData GetData()
        {
            return null;
        }

        public GameObject GetObject()
        {
            return gameObject;
        }

        protected virtual void OnStart()
        {
        }

        protected Element GetRandomElement()
        {
            return (Element)Random.Range(0, 5);
        }
    }
}