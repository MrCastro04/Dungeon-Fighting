using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private Vector3 _moveVector;
        private float _agentSpeed;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            MovePlayer();
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            _moveVector = new Vector3(input.x, 0f, input.y);
        }

        private void MovePlayer()
        {
            var offset = _moveVector * (Time.deltaTime * _agent.speed);

            _agent.Move(offset);
        }
    }
}