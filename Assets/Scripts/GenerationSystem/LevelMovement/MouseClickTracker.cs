using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class MouseClickTracker : MonoBehaviour
    {
        public UnityAction<Vector3> MoveClick;
        public UnityAction<InteractiveObject, Vector3> ObjectClick;
 
        private Vector3 _mousePosition;
        private bool _pointerOverUi;
        
        private void FixedUpdate()
        {
            _pointerOverUi = EventSystem.current.IsPointerOverGameObject();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(_pointerOverUi)
                    return;
                
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                if (CastRay(out InteractiveObject data))
                {
                    ObjectClick?.Invoke(data, _mousePosition);
                }
                else
                {
                    MoveClick?.Invoke(_mousePosition);
                }
            }
        }

        private bool CastRay(out InteractiveObject data)
        {
            data = null;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider != null && hit.collider.TryGetComponent(out InteractiveObject detector))
            {
                data = detector.GetObject();
                return true;
            }

            return false;
        }
    }
}