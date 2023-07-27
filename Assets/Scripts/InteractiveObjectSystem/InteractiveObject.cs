using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public abstract class InteractiveObject : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private string _stats;

        public string Name => _name;

        public string Stats => _stats;
        
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}