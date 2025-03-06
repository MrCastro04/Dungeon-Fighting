using System.Collections;
using Character.FOR_ALL_CHARACTERS;
using Core;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Player
{
    [RequireComponent(typeof(Combat))]
    public class Ability : MonoBehaviour
    {
        [SerializeField] protected float _hitRadius = 1.5f;
        [SerializeField] protected float _abilityDuration = 4f;
        [SerializeField] protected float _abilityCooldown = 5f;

        protected Combat _combatCmp;
        protected BubbleEvent _bubbleEvent;
        protected Animator _animatorCmp;
        protected bool _isAbilityActive = false;
        protected float _currentDuration;
        protected float _currentCooldown;

        public bool IsAbilityActive => _isAbilityActive;
        public Animator AnimatorCmp => _animatorCmp;

        protected virtual void Awake()
        {
            _currentCooldown = _abilityCooldown;

            _combatCmp = GetComponent<Combat>();

            _bubbleEvent = GetComponentInChildren<BubbleEvent>();

            _animatorCmp = GetComponentInChildren<Animator>();
        }

        protected virtual void OnEnable()
        {
            _bubbleEvent.OnBubbleAbilityStart += HandlerBubbleAbilityStart;
            _bubbleEvent.OnBubbleAbilityEnd += HandlerBubbleAbilityEnd;
            _bubbleEvent.OnBubbleHitAbilityAttack += HandlerHitAbilityAttack;
        }

        protected virtual void OnDisable()
        {
            _bubbleEvent.OnBubbleAbilityStart -= HandlerBubbleAbilityStart;
            _bubbleEvent.OnBubbleAbilityEnd -= HandlerBubbleAbilityEnd;
            _bubbleEvent.OnBubbleHitAbilityAttack -= HandlerHitAbilityAttack;
        }

        public virtual void HandlerAbility(InputAction.CallbackContext context)
        {
            if (context.performed == false || _isAbilityActive || !IsAbilityReady() || _combatCmp.IsAttacking)
            {
                return;
            }

            EventManager.RaisebilityButtonClick();

            _isAbilityActive = true;

            SetAbilityToken(_isAbilityActive);
        }

        public void SetAbilityToken(bool value)
        {
            _animatorCmp.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, value);
        }

        protected virtual void HandlerBubbleAbilityStart()
        {
            _currentDuration += (Time.deltaTime + 1);
        }

        protected virtual void HandlerBubbleAbilityEnd()
        {
            if (_currentDuration >= _abilityDuration)
            {
                _isAbilityActive = false;

                _animatorCmp.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, _isAbilityActive);

                _currentDuration = 0f;

                StartCoroutine(StartAbilityCooldownTimer());
            }
        }

        private void HandlerHitAbilityAttack()
        {
            RaycastHit[] targets = Physics.SphereCastAll(

                transform.position,

                _hitRadius,

                transform.forward,

                _hitRadius);

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

                health.TakeDamage(_combatCmp.Damage);
            }
        }

        protected virtual IEnumerator StartAbilityCooldownTimer()
        {
            _currentCooldown = 0f;

            while (IsAbilityReady() == false)
            {
                _currentCooldown += Time.deltaTime;

                yield return null;
            }

            EventManager.RaiseOnPlayerAbilityReady();
        }

        public bool IsAbilityReady()
        {
            return _currentCooldown >= _abilityCooldown;
        }
    }
}
