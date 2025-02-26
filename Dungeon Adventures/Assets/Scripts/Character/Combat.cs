using System.Collections;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private float _playerAbilityTime;

        private BubbleEvent _bubbleEvent;
        private Animator _animator;
        private float _damage;
        private bool _isAttacking = false;
        private bool _isAbilityActive = false;

        public float Damage
        {
            set { _damage = value; }
        }

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

            _bubbleEvent.OnBubbleAbilityStart += HandleBubbleAbilityStart;

            _bubbleEvent.OnBubbleAbilityEnd += HandleBubbleAbilityEnd;
        }

        private void OnDisable()
        {
            _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;

            _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;

            _bubbleEvent.OnBubbleHitAttack -= HandleBubbleHitAttack;

            _bubbleEvent.OnBubbleAbilityStart -= HandleBubbleAbilityStart;

            _bubbleEvent.OnBubbleAbilityEnd -= HandleBubbleAbilityEnd;
        }

        public void HandleAttack(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;

            StartAttack();
        }

        public void HandleAbility(InputAction.CallbackContext context)
        {
            if (context.performed == false) return;

            StartAbility();
        }

        public void StartAttack()
        {
            if (_isAttacking) return;

            _animator.SetFloat(Constants.ANIMATOR_SPEED_PARAM, 0f);

            _animator.SetTrigger(Constants.ANIMATOR_ATTACK_PARAM);
        }

        public void StartAbility()
        {
            if (_isAbilityActive) return;

            _animator.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, true);
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

        private void HandleBubbleAbilityStart()
        {
            _isAbilityActive = true;
        }

        private void HandleBubbleAbilityEnd()
        {
            StartCoroutine(ResetAbility(_playerAbilityTime));
        }

        private void HandleBubbleHitAttack()
        {
            RaycastHit[] targets = GetTargets();

            foreach (var target in targets)
            {
                if (CompareTag(target.transform.tag))
                {
                    continue;
                }

                Health health = target.transform.gameObject.GetComponent<Health>();

                if (health == null)
                {
                    continue;
                }

                health.TakeDamage(_damage);
            }
        }

        private IEnumerator ResetAbility( float abilityTime )
        {
            yield return new WaitForSeconds( abilityTime );

            _animator.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, false);

            _isAbilityActive = false;
        }

        private RaycastHit[] GetTargets()
        {
            if (_isAbilityActive)
            {
                RaycastHit[] targets = Physics.SphereCastAll(

                    transform.position,

                    1.5f,

                    transform.forward,

                    1.5f);

                return targets;
            }

            else
            {
                RaycastHit[] targets = Physics.BoxCastAll(

                    transform.position + transform.forward,

                    transform.forward / 2,

                    transform.forward,

                    transform.rotation,

                    1f);

                return targets;
            }
        }
    }
}