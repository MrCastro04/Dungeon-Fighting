using System;
using Core;
using Uitility;
using UnityEngine;

namespace Character
{
    public class Health : MonoBehaviour
    {
        private BubbleEvent _bubbleEventCmp;
        private Animator _animatorCmp;
        private float _heathPoints;
        private bool _isDefeated = false;

        public event Action OnStartEnemyDefeated;

        public float HeathPoints
        {
            get
            {
                return _heathPoints;
            }
            set
            {
                _heathPoints = value;
            }
        }

        private void Awake()
        {
            _animatorCmp = GetComponentInChildren<Animator>();

            _bubbleEventCmp = GetComponentInChildren<BubbleEvent>();
        }

        private void OnEnable()
        {
            _bubbleEventCmp.OnBubbleDefeat += HandleOnDefeat;
        }

        private void OnDisable()
        {
            _bubbleEventCmp.OnBubbleDefeat -= HandleOnDefeat;
        }

        public void TakeDamage(float damageAmount)
        {
            _heathPoints = Mathf.Max(_heathPoints - damageAmount, 0f);

            if (CompareTag(Constants.PLAYER_TAG))
            {
                EventManager.RaiseChangePlayerHealth(_heathPoints);
            }

            if (_heathPoints == 0)
            {
               Defeat();
            }
        }

        private void Defeat()
        {
            if(_isDefeated) return;

            if (CompareTag(Constants.ENEMY_TAG))
            {
                OnStartEnemyDefeated?.Invoke();
            }

            _isDefeated = true;

            _animatorCmp.SetTrigger(Constants.DEFEAT_ANIMATOR_PARAM);
        }

        private void HandleOnDefeat()
        {
            Destroy(gameObject);
        }
    }
}