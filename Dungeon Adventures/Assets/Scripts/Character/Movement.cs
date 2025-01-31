using Uitility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private Vector3 _moveVector;
        private float _agentSpeed;
        private bool _isMoving = false;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            MovePlayer();

            UpdateAnimations();

            if (CompareTag(Constants.PLAYER_TAG))
            {
                RotateAgentByOffset(_moveVector);
            }
        }

        public void HandleMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _isMoving = true;
            }

            if (context.canceled)
            {
                _isMoving = false;
            }
            
            Vector2 input = context.ReadValue<Vector2>();

            _moveVector = new Vector3(input.x, 0f, input.y);
        }

        public void MoveAgentByOffset(Vector3 offset)
        {
            _agent.Move(offset);

            _isMoving = true;
        }

        public void MoveAgentByDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);

            _isMoving = true;
        }

        public void StopAgentMoving()
        {
            _agent.ResetPath();

            _isMoving = false;
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

        private void UpdateAnimations()
        {
            float speed = _animator.GetFloat(Constants.SPEED_ANIMATOR_PARAM);

            float smooth = Time.deltaTime * _agent.acceleration;

            if (_isMoving)
            {
                speed += smooth;
            }

            else
            {
                speed -= smooth;
            }

            speed = Mathf.Clamp01(speed);

            _animator.SetFloat(Constants.SPEED_ANIMATOR_PARAM, speed);
        }
    }
}