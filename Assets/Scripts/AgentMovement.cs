using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class AgentMovement : MonoBehaviour
    {
        [SerializeField] private GameObject _point;
        
        private Vector3 _target;
        private NavMeshAgent _agent;

        private void Awake()
        {
            _point.SetActive(false);
            
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            SetTargetPosition();
            SetAgentPosition();
            CheckDistance();
        }

        private void SetTargetPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _point.transform.position = new Vector3(_target.x, _target.y, 0);
                _point.SetActive(true);
            }
        }

        private void SetAgentPosition()
        {
            _agent.SetDestination(new Vector3(_target.x, _target.y, transform.position.z));
        }

        private void CheckDistance()
        {
            var distance = Vector3.Distance(transform.position, _target);
            
            if (distance < 0.1f)
                _point.SetActive(false);
        }
    }
}