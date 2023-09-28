using IJunior.TypedScenes;
using UnityEngine;

namespace Assets.Infrastructure.SceneLoadHandler
{
    public class LevelEntry : MonoBehaviour, ISceneLoadHandler<PlayerLevelData>
    {
        [SerializeField] private LevelRootBase _levelRoot;
        
        public void OnSceneLoaded(PlayerLevelData playerLevelData)
        {
            _levelRoot.Init(playerLevelData);
        }
    }
}