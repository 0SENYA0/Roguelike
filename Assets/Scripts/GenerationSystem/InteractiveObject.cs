using UnityEngine;

namespace Assets.Scripts.GenerationSystem
{
    public class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private string _stats;

        public string Name => _name;

        public string Stats => _stats;


        public InteractiveObject GetObject()
        {
            return this;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}