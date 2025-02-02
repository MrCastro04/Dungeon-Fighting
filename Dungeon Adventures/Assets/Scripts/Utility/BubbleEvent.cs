using UnityEngine;
using UnityEngine.Events;

namespace Uitility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack;
        public event UnityAction OnBubbleEndAttack;
        public event UnityAction OnBubblHitAttack;

        public void HandleBubbleStartAttack()
        {
            OnBubbleStartAttack?.Invoke();
        }

        public void HandleBubbleEndAttack()
        {
            OnBubbleEndAttack?.Invoke();
        }

        public void HandleBubblHitAttack()
        {
            OnBubblHitAttack?.Invoke();
        }
    }
}