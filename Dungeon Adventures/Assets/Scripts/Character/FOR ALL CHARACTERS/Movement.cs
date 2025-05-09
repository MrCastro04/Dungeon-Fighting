using Core;
using Interfaces;
using Uitility;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Utility;

namespace Character.FOR_ALL_CHARACTERS
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Movement : MonoBehaviour
    {
        private BubbleEvent _bubbleEvent;
        private Animator _animator;
        private NavMeshAgent _agent;
        private Vector3 _moveVector;
        private float _originAgentSpeed;
        private bool _isMoving = false;
        private bool _canRotate = true;

        public NavMeshAgent NavMeshAgent => _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _animator = GetComponentInChildren<Animator>();

            _bubbleEvent = GetComponentInChildren<BubbleEvent>();

            _originAgentSpeed = _agent.speed;
        }

        private void OnEnable()
        {
            if (CompareTag(Constants.TAG_BOSS))
            {
                _bubbleEvent.OnBubbleAbilityStart += HandleBubbleAbilityStart;
                _bubbleEvent.OnBubbleAbilityEnd += HandleBubbleAbilityEnd;
            }

            _bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack += HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleStartAnimationDefeat += HandleBubbleStartAnimationDefeat;
        }

        private void OnDisable()
        {
            if (CompareTag(Constants.TAG_BOSS))
            {
                _bubbleEvent.OnBubbleAbilityStart -= HandleBubbleAbilityStart;
                _bubbleEvent.OnBubbleAbilityEnd -= HandleBubbleAbilityEnd;
            }

            _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleStartAnimationDefeat -= HandleBubbleStartAnimationDefeat;
        }

        private void Update()
        {
            MovePlayer();

            UpdateAnimations();

            if (CompareTag(Constants.TAG_PLAYER))
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
            if ( _canRotate == false || offset == Vector3.zero) return;

            var normal = Time.deltaTime * _agent.angularSpeed;

            Quaternion startQuaternion = transform.rotation;

            Quaternion endQuaternion = Quaternion.LookRotation(offset);

            transform.rotation = Quaternion.Lerp(startQuaternion,endQuaternion, normal);
        }

        private void MovePlayer()
        {
            var offset = _moveVector * (Time.deltaTime * _agent.speed);

            _agent.Move(offset);
        }

        private void UpdateAnimations()
        {
            float speed = _animator.GetFloat(Constants.ANIMATOR_SPEED_PARAM);

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

            _animator.SetFloat(Constants.ANIMATOR_SPEED_PARAM, speed);
        }

        private void HandleBubbleStartAttack()
        {
            _agent.speed = 0f;

            _canRotate = false;
        }

        private void HandleBubbleEndAttack()
        {
            _agent.speed = _originAgentSpeed;

            _canRotate = true;
        }

        private void HandleBubbleAbilityStart()
        {
            HandleBubbleStartAttack();
        }

        private void HandleBubbleAbilityEnd()
        {
            HandleBubbleEndAttack();
        }

        private void HandleBubbleStartAnimationDefeat()
        {
            HandleBubbleStartAttack();
        }
    }
}