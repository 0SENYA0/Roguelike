using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public interface IInteractiveObject
    {
        ObjectType ObjectType { get; }
        
        void DestroyObject();
        InteractiveObjectData GetData();
        GameObject GetObject();
    }
}