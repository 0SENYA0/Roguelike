using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public interface IInteractiveObject
    {
        void DestroyObject();
        InteractiveObjectData GetData();
        GameObject GetObject();
    }
}