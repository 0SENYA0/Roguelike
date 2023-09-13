using Assets.Fight.Element;
using Lean.Localization;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Assets.Scripts.InteractiveObjectSystem
{
    [RequireComponent(typeof(Collider2D))]
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private ObjectType _type;
        [SerializeField]
        [LeanTranslationName]
        [FormerlySerializedAs("phraseName")]
        [FormerlySerializedAs("translationTitle")]
        protected string _translationName;
        [Multiline] [SerializeField] protected string _data;

        public ObjectType Type => _type;

        private Element _element;
        public string Data => _data;
        
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
            return this.gameObject;
        }

        protected virtual void OnStart()
        {
        }

        protected void AddInfoInData(string data) =>
            _data = _data + " " + data;

        protected Element GetRandomElement()
        {
            return (Element)Random.Range(0, 5);
        }
    }
}