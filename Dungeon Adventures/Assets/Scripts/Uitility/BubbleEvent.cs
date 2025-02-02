using UnityEngine;
using UnityEngine.Events;

namespace Uitility
{
    public class BubbleEvent : MonoBehaviour
    {
        public event UnityAction OnBubbleStartAttack;
        public event UnityAction OnBubbleEndAttack;
        public event UnityAction OnBubblHitAttack;

    }
}