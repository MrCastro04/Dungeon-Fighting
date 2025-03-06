using System.Collections;
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

        public int DesiredHitCounter => _desiredHitCounter;
        public int CurrentHitCounter => _currentHitCounter;

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
            _canCountHitCounters = false;

            base.HandlerBubbleAbilityStart();
        }

        protected override void HandlerBubbleAbilityEnd()
        {
            _canCountHitCounters = true;

            _currentHitCounter = 0;

            _desiredHitCounter = 0;

            _desiredHitCounter = GetRandomHitCounter();

            Debug.Log($"DesiredHitCounters - {_desiredHitCounter}");

            _isAbilityActive = false;

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
            return Random.Range(1, 5);
        }

        private void HandlerChangeBossHitCounters()
        {
            if (_canCountHitCounters && IsCurrentCountersEqualOrGreatThenDesired() == false)
            {
                _currentHitCounter ++;

                Debug.Log($"After hit counter - {_currentHitCounter}");
            }
        }
    }
}