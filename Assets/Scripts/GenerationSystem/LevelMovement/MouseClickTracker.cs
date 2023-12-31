using Assets.Scripts.InteractiveObjectSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class MouseClickTracker : MonoBehaviour
    {
        private readonly int _mouseKey = 0;

        private Vector3 _mousePosition;
        private Camera _camera;

        public UnityAction<Vector3> MoveClick;
        public UnityAction<InteractiveObject, Vector3> ObjectClick;

        private void Start() =>
            _camera = Camera.main;

        private void Update()
        {
            if (Input.GetMouseButtonDown(_mouseKey))
            {
                bool isPointerOverUi = EventSystem.current.IsPointerOverGameObject();

                if (isPointerOverUi)
                    return;

                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

                if (TryGetInteractiveObject(out InteractiveObject selectedObject))
                    ObjectClick?.Invoke(selectedObject, _mousePosition);
                else
                    MoveClick?.Invoke(_mousePosition);
            }
        }

        private bool TryGetInteractiveObject(out InteractiveObject data)
        {
            data = null;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

            if (hit.collider != null && hit.collider.TryGetComponent(out InteractiveObject selectedObject))
            {
                data = selectedObject;
                return true;
            }

            return false;
        }
    }
}