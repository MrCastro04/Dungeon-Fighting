using UnityEngine;
using UnityEngine.Events;

namespace Uitility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => {};
        public event UnityAction OnBubbleEndAttack = () => {};
        public event UnityAction OnBubbleHitAttack = () => {};
        public event UnityAction OnBubbleAbilityStart = () => {};
        public event UnityAction OnBubbleAbilityEnd = () => {};
        public event UnityAction OnBubbleHitAbilityAttack = () => {};
        public event UnityAction OnBubbleDefeat = () => {};

        private void OnStartAttack()
        {
            OnBubbleStartAttack?.Invoke();
        }

        private void OnEndAttack()
        {
            OnBubbleEndAttack?.Invoke();
        }

        private void OnHitAttack()
        {
            OnBubbleHitAttack?.Invoke();
        }

        private void OnDefeat()
        {
            OnBubbleDefeat?.Invoke();
        }

        private void OnAbilityStart()
        {
            OnBubbleAbilityStart?.Invoke();
        }

        private void OnHitAbility()
        {
            OnBubbleHitAbilityAttack?.Invoke();
        }

        private void OnAbilityEnd()
        {
            OnBubbleAbilityEnd?.Invoke();
        }
    }
}