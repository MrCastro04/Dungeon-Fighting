using Character.Player;
using Core;
using Uitility;
using UnityEngine;

namespace Character.Boss
{
    public class BossAbility : Ability
    {
        private int _desiredHitCounter;
        private int _currentHitCounter;
        private bool _canCountHitCounters = true;

        protected override void Awake()
        {
            base.Awake();

            _currentHitCounter = _desiredHitCounter;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventManager.OnChangeBossHitCounters += HandlerChangeBossHitCounters;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventManager.OnChangeBossHitCounters -= HandlerChangeBossHitCounters;
        }

        protected override void HandlerBubbleAbilityStart()
        {
            _isAbilityActive = true;

            _canCountHitCounters = false;

            base.HandlerBubbleAbilityStart();
        }

        protected override void HandlerBubbleAbilityEnd()
        {
            _isAbilityActive = false;

            _canCountHitCounters = true;

            _currentHitCounter = 0;

            _desiredHitCounter = 0;

            _desiredHitCounter = GetRandomHitCounter();

            _animatorCmp.SetBool(Constants.ANIMATOR_ABILITY_TOKEN, _isAbilityActive);
        }

        public bool IsCurrentCountersEqualOrGreatThenDesired()
        {
            if (_currentHitCounter >= _desiredHitCounter)
            {
                return true;
            }

            if (_currentHitCounter < _desiredHitCounter)
            {
                return false;
            }

            return false;
        }

        private int GetRandomHitCounter()
        {
            return Random.Range(1, 3);
        }

        private void HandlerChangeBossHitCounters()
        {
            if (_canCountHitCounters && IsCurrentCountersEqualOrGreatThenDesired() == false)
            {
                _currentHitCounter ++;
            }
        }
    }
}