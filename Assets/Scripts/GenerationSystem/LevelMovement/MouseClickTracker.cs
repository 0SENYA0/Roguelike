using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class MouseClickTracker : MonoBehaviour
    {
        private readonly int _mouseKey = 0;
        
        public UnityAction<Vector3> MoveClick;
        public UnityAction<IInteractiveObject, Vector3> ObjectClick;

        private Vector3 _mousePosition;
        private bool _isPointerOverUi;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(_mouseKey))
            {
                bool _isPointerOverUi = EventSystem.current.IsPointerOverGameObject();
                
                if(_isPointerOverUi)
                    return;
                
                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                
                if (TryGetInteractiveObject(out IInteractiveObject selectedObject))
                {
                    ObjectClick?.Invoke(selectedObject, _mousePosition);
                }
                else
                {
                    MoveClick?.Invoke(_mousePosition);
                }
            }
        }

        private bool TryGetInteractiveObject(out IInteractiveObject data)
        {
            data = null;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider != null && hit.collider.TryGetComponent(out IInteractiveObject selectedObject))
            {
                data = selectedObject;
                return true;
            }

            return false;
        }
    }
}