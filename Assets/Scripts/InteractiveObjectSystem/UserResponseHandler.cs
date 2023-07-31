using Assets.Scripts.GenerationSystem.LevelMovement;
using UnityEngine;

namespace Assets.Scripts.InteractiveObjectSystem
{
    public class UserResponseHandler : MonoBehaviour
    {
        [SerializeField] private CanvasInfoPanel _infoPanel;
        [SerializeField] private MouseClickTracker _clickTracker;
        [SerializeField] private InteractiveObjectHandler interactiveObjectHandler;

        private Vector3 _targetPosition;
        private IInteractiveObject _selectedObject;

        private void Awake()
        {
            _clickTracker.ObjectClick += ShowPanel;
        }

        private void OnDestroy()
        {
            _clickTracker.ObjectClick -= ShowPanel;
        }

        private void ShowPanel(IInteractiveObject selectedObject, Vector3 position)
        {
            _infoPanel.ShowPanel(selectedObject);
            _targetPosition = position;
            _selectedObject = selectedObject;
            _infoPanel.UserResponse += MoveFixedAgent;
        }

        private void MoveFixedAgent(bool isMoveToTarget)
        {
            _infoPanel.UserResponse -= MoveFixedAgent;
            
            if (isMoveToTarget)
                interactiveObjectHandler.ProduceInteraction(_selectedObject, _targetPosition);           
        }
    }
}