using UnityEngine;
using UnityEngine.Events;

namespace Uitility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack = () => {};
        public event UnityAction OnBubbleEndAttack = () => {};
        public event UnityAction OnBubbleHitAttack = () => {};
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
    }
}