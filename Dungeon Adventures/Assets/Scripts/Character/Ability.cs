using Character;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ability : MonoBehaviour
{
    [SerializeField] private float _abilityDuration;
    [SerializeField] private float _abilityCooldown;

    private Combat _combatCmp;
    private BubbleEvent _bubbleEvent;
    private Animator _animatorCmp;
    private bool _isAbilityActive = false;
    private float _hitRadius = 1.5f;
    private float _currentDuration;
    private float _currentCooldown;

    public float GetFullCurrentCooldown => _currentCooldown = _abilityDuration;

    private void Awake()
    {
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
        if(context.performed == false || _isAbilityActive || !IsAbilityReady())
        {
            return;
        }

        _isAbilityActive = true;

        _currentCooldown = 0;

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

            StartAbilityCooldownTimer();
        }
    }

    private void HandlerHitAbilityAttack()
    {
        RaycastHit[] targets = Physics.SphereCastAll(

            transform.position,

            _hitRadius,

            transform.forward
        );

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

    private void StartAbilityCooldownTimer()
    {
        while (IsAbilityReady() == false)
        {
            _currentCooldown += (Time.deltaTime + 1);
        }
    }

    private bool IsAbilityReady()
    {
        return _currentCooldown >= _abilityCooldown;
    }
}
