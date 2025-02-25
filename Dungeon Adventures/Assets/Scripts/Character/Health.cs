using System;
using Core;
using Uitility;
using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class Health : MonoBehaviour
    {
        public event Action OnStartEnemyDefeated;

        [NonSerialized] public Slider SliderCmp;

        public float HealthPoints;

        private BubbleEvent _bubbleEventCmp;
        private Animator _animatorCmp;
        private bool _isDefeated = false;

        private void Awake()
        {
            _animatorCmp = GetComponentInChildren<Animator>();

            _bubbleEventCmp = GetComponentInChildren<BubbleEvent>();

            SliderCmp = GetComponentInChildren<Slider>();
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
            HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0f);

            if (CompareTag(Constants.TAG_PLAYER))
            {
                EventManager.RaiseChangePlayerHealth(HealthPoints);
            }

            if (SliderCmp != null)
            {
                SliderCmp.value -= damageAmount;
            }

            if (HealthPoints == 0)
            {
               Defeat();
            }
        }

        private void Defeat()
        {
            if(_isDefeated) return;

            if (CompareTag(Constants.TAG_ENEMY))
            {
                OnStartEnemyDefeated?.Invoke();
            }

            _isDefeated = true;

            _animatorCmp.SetTrigger(Constants.ANIMATOR_DEFEAT_PARAM);
        }

        private void HandleOnDefeat()
        {
            Destroy(gameObject);
        }
    }
}