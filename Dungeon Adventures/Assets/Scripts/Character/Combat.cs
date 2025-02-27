using System;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float Damage;

        private BubbleEvent _bubbleEvent;
        private Animator _animator;
        private bool _isAttacking = false;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            _bubbleEvent = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            _bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack += HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleHitAttack += HandleBubbleHitAttack;
        }

        private void OnDisable()
        {
            _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleHitAttack -= HandleBubbleHitAttack;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (_isAttacking) return;

            _animator.SetFloat(Constants.ANIMATOR_SPEED_PARAM, 0f);

            _animator.SetTrigger(Constants.ANIMATOR_ATTACK_PARAM);
        }

        public void CancelAttack()
        {
            _animator.ResetTrigger(Constants.ANIMATOR_ATTACK_PARAM);
        }

        private void HandleBubbleStartAttack()
        {
            _isAttacking = true;
        }

        private void HandleBubbleEndAttack()
        {
            _isAttacking = false;
        }

        private void HandleBubbleHitAttack()
        {
            var targets = Physics.BoxCastAll(

                transform.position + transform.forward,

                transform.forward / 2,

                transform.forward,

                transform.rotation,

                1f);

            foreach (var target in targets)
            {
                if (CompareTag(target.transform.tag)) continue;

                var health = target.transform.gameObject.GetComponent<Health>();

                if (health == null) continue;

                health.TakeDamage(Damage);
            }
        }
    }
}