using Uitility;
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

            if (CompareTag(Constants.PLAYER_TAG))
            {
                RotateAgentByOffset(_moveVector);
            }
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();

            _moveVector = new Vector3(input.x, 0f, input.y);
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            _agent.Move(offset);
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void RotateAgentByOffset(Vector3 offset)
        {
            if (offset == Vector3.zero) return;

            var normal = Time.deltaTime * _agent.angularSpeed;

            Quaternion startQuaternion = transform.rotation;

            Quaternion endQuaternion = Quaternion.LookRotation(offset);

            transform.rotation = Quaternion.Lerp(startQuaternion,endQuaternion, normal);
        }

        public bool ReachedDestination()
        {
            if (_agent.pathPending)
            {
                return false;
            }

            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                return false;
            }

            if (_agent.hasPath || _agent.velocity.sqrMagnitude != 0f)
            {
                return false;
            }

            return true;
        }


        private void MovePlayer()
        {
            var offset = _moveVector * (Time.deltaTime * _agent.speed);

            _agent.Move(offset);
        }
    }
}