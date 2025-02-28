using System.Collections;
using Core;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    [RequireComponent(typeof(Combat))]
    public class Ability : MonoBehaviour
    {
        [SerializeField] private float _abilityDuration = 4f;
        [SerializeField] private float _abilityCooldown = 5f;
        [SerializeField] private float _hitRadius = 1.5f;

        private Combat _combatCmp;
        private BubbleEvent _bubbleEvent;
        private Animator _animatorCmp;
        private bool _isAbilityActive = false;
        private float _currentDuration;
        private float _currentCooldown;

        public bool IsAbilityActive => _isAbilityActive;

        private void Awake()
        {
            _currentCooldown = _abilityCooldown;

            _combatCmp = GetComponent<Combat>();

            _bubbleEvent = GetComponentInChildren<BubbleEvent>();

            _animatorCmp = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            _bubbleEvent.OnBubbleAbilityStart += HandlerBubbleAbilityStart;
            _bubbleEvent.OnBubbleAbilityEnd += HandlerBubbleAbilityEnd;
            _bubbleEvent.OnBubbleHitAbilityAttack += HandlerHitAbilityAttack;
        }

        private void OnDisable()
        {
            _bubbleEvent.OnBubbleAbilityStart -= HandlerBubbleAbilityStart;
            _bubbleEvent.OnBubbleAbilityEnd -= HandlerBubbleAbilityEnd;
            _bubbleEvent.OnBubbleHitAbilityAttack -= HandlerHitAbilityAttack;
        }

        public void HandlerAbility(InputAction.CallbackContext context)
        {
            if (context.performed == false || _isAbilityActive || !IsAbilityReady())
            {
                return;
            }

            EventManager.RaisebilityButtonClick();

            _isAbilityActive = true;

            _animatorCmp.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, true);
        }

        private void HandlerBubbleAbilityStart()
        {
            _currentDuration += (Time.deltaTime + 1);
        }

        private void HandlerBubbleAbilityEnd()
        {
            if (_currentDuration >= _abilityDuration)
            {
                _isAbilityActive = false;

                _animatorCmp.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, false);

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

        private IEnumerator StartAbilityCooldownTimer()
        {
            _currentCooldown = 0f;

            while (IsAbilityReady() == false)
            {
                _currentCooldown += Time.deltaTime;

                yield return null;
            }

            EventManager.RaiseOnAbilityReady();
        }

        private bool IsAbilityReady()
        {
            return _currentCooldown >= _abilityCooldown;
        }
    }
}
