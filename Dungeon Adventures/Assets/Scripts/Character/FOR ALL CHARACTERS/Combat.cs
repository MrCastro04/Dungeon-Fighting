using System;
using Core;
using Interfaces;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Character.FOR_ALL_CHARACTERS
{
    public class Combat : MonoBehaviour
    {
        [NonSerialized] public float Damage;

        protected BubbleEvent _bubbleEvent;
        protected Animator _animator;

        public bool IsAttacking { get; protected set; }

        protected virtual void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            _bubbleEvent = GetComponentInChildren<BubbleEvent>();
        }

        protected virtual void OnEnable()
        {
            _bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack += HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleHitAttack += HandleBubbleHitAttack;
        }

        protected virtual void OnDisable()
        {
            _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;
            _bubbleEvent.OnBubbleHitAttack -= HandleBubbleHitAttack;
        }

        public void HandleAttack (InputAction.CallbackContext context)
        {
            if (context.performed == false) return;

            StartAttack();
        }

        public void StartAttack()
        {
            if (IsAttacking) return;

            _animator.SetFloat(Constants.ANIMATOR_SPEED_PARAM, 0f);

            _animator.SetTrigger(Constants.ANIMATOR_ATTACK_PARAM);
        }

        public void CancelAttack()
        {
            _animator.ResetTrigger(Constants.ANIMATOR_ATTACK_PARAM);
        }

        protected virtual void HandleBubbleStartAttack()
        {
            IsAttacking = true;
        }

         protected void HandleBubbleEndAttack()
        {
            IsAttacking = false;
        }

        protected virtual void HandleBubbleHitAttack()
        {
            EventManager.RaiseSoundOnMissHit( SoundActionType.MissHit,
                GetComponent<IControllerType>().GetSelfType() );

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

                if (health == null)
                {
                    continue;
                }

                health.TakeDamage(Damage);
            }
        }
    }
}