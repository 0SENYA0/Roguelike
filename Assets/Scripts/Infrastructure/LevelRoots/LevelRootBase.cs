using Assets.Infrastructure.SceneLoadHandler;
using UnityEngine;

namespace Assets.Infrastructure
{
    public abstract class LevelRootBase : MonoBehaviour
    {
        public abstract void Init(PlayerLevelData playerLevelData);
    }
}