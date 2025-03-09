using System.Collections;
using Character.FOR_ALL_CHARACTERS;
using Core;
using Interfaces;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility;

namespace Character.Player
{
    [RequireComponent(typeof(Combat))]
    public class Ability : MonoBehaviour
    {
        [SerializeField] private float _hitRadius = 1.5f;
        [SerializeField] private float _abilityDuration = 4f;
        [SerializeField] private float _abilityCooldown = 5f;

        private Combat _combatCmp;
        protected BubbleEvent _bubbleEvent;
        protected Animator _animatorCmp;
        protected bool _isAbilityActive = false;
        private float _currentDuration;
        private float _currentCooldown;

        public bool IsAbilityActive => _isAbilityActive;

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

        public bool IsAbilityReady()
        {
            return _currentCooldown >= _abilityCooldown;
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
            EventManager.RaiseSoundOnAbilityWindCut(SoundActionType.AbilityWindCut, GetComponent<IControllerType>().GetSelfType());

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

        private IEnumerator StartAbilityCooldownTimer()
        {
            _currentCooldown = 0f;

            while (IsAbilityReady() == false)
            {
                _currentCooldown += Time.deltaTime;

                yield return null;
            }

            EventManager.RaiseOnPlayerAbilityReady();
        }
    }
}
