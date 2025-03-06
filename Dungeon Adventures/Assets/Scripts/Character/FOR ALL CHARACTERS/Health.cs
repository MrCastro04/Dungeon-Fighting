using System;
using Character.Boss;
using Core;
using Uitility;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Character.FOR_ALL_CHARACTERS
{
    public class Health : MonoBehaviour
    {
        public event Action OnStartEnemyDefeated;

        [NonSerialized] public float OriginHealthPoints;
        [NonSerialized] public float HealthPoints;
        [NonSerialized] public Slider SliderCmp;

        public float HealAmount;
        public int PotionCount;

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

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (context.performed == false || PotionCount <= 0) return;

            UsePotion();
        }

        public void TakeDamage(float damageAmount)
        {
            HealthPoints = Mathf.Max(HealthPoints - damageAmount, 0f);

            if (CompareTag(Constants.TAG_PLAYER))
            {
                EventManager.RaiseChangePlayerHealth(HealthPoints);
            }

            if (CompareTag(Constants.TAG_BOSS))
            {
                EventManager.RaiseOnChangeBossHitCounters();
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

        public bool IsHealthLesserRequiredPercentage(IController anyController, float requiredPercentage)
        {
            float bossOriginHealth = anyController.HealthCmp.OriginHealthPoints;

            float bossCurrentHealth = anyController.HealthCmp.HealthPoints;

            float bossCurrentHealthPercent = Mathf.Clamp01(bossCurrentHealth / bossOriginHealth);

            if (bossCurrentHealthPercent < requiredPercentage)
            {
                return true;
            }

            return false;
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

        private void UsePotion()
        {
            HealthPoints += HealAmount;

            PotionCount--;

            PotionCount = Mathf.Max(PotionCount, 0);

            EventManager.RaiseChangePlayerHealth(HealthPoints);

            EventManager.RaiseChangePlayerPotionCount(PotionCount);
        }

        private void HandleOnDefeat()
        {
            Destroy(gameObject);
        }
    }
}