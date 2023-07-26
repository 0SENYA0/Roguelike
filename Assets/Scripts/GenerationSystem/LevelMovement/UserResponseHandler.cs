using UnityEngine;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class UserResponseHandler : MonoBehaviour
    {
        [SerializeField] private CanvasInfoPanel _infoPanel;
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private BattleHandler _battleHandler;

        private Vector3 _targetPosition;
        private InteractiveObject _targetObject;

        private void Awake()
        {
            _clickTracker.ObjectClick += ShowPanel;
        }

        private void OnDestroy()
        {
            _clickTracker.ObjectClick -= ShowPanel;
        }

        private void ShowPanel(InteractiveObject interactiveObject, Vector3 position)
        {
            _infoPanel.ShowPanel(interactiveObject);
            _targetPosition = position;
            _targetObject = interactiveObject;
            _infoPanel.UserResponse += MoveFixedAgent;
        }

        private void MoveFixedAgent(bool isMoveToTarget)
        {
            _infoPanel.UserResponse -= MoveFixedAgent;
            
            if (isMoveToTarget)
                _battleHandler.DisplayBattle(_targetObject, _targetPosition);           
        }
    }
}