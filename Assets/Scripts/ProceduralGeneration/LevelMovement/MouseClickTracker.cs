using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.ProceduralGeneration.LevelMovement
{
    public class MouseClickTracker : MonoBehaviour
    {
        public UnityAction<Vector3> Click;
        
        private Vector3 _mousePosition;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Click?.Invoke(_mousePosition);                
            }
        }
    }
}