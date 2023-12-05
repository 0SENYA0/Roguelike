using UnityEngine;

namespace Assets.Scripts.GenerationSystem.LevelMovement
{
    public class CharacterDestinationMarker : MonoBehaviour
    {
        [SerializeField] private Transform _characterTarget;
        [SerializeField] private float _hidingDistance = 1.5f;
        [SerializeField] private float _blackAtDistance = 1f;
        [SerializeField] private SpriteRenderer _renderer;

        private void Update()
        {
            float distance = (_characterTarget.position - transform.position).magnitude;
            distance -= _blackAtDistance;
            
            float percentage = distance / _hidingDistance;
            percentage = Mathf.Clamp(percentage, 0, 1);

            _renderer.material.color = new Color(1, 1, 1, percentage);
        }

        public void SetPosition(Vector3 position) =>
            transform.position = new Vector3(position.x, position.y, 0);
    }
}