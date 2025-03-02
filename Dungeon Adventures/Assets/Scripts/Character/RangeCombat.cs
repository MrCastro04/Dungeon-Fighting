using System;
using UnityEngine;

namespace Character
{
    public class RangeCombat : Combat
    {
        [SerializeField] private Transform _firePoint;

        [NonSerialized] public float RangeDamage;
        [NonSerialized] public float FireRate;
        [NonSerialized] public float NextFireTime;
        [NonSerialized] public float ProjectileSpeed;

        protected override void OnEnable()
        {
            _bubbleEvent.OnBubbleStartAttack += HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack += HandleBubbleEndAttack;
        }

        protected override void OnDisable()
        {
            _bubbleEvent.OnBubbleStartAttack -= HandleBubbleStartAttack;
            _bubbleEvent.OnBubbleEndAttack -= HandleBubbleEndAttack;
        }

        protected override void HandleBubbleStartAttack()
        {
            base.HandleBubbleStartAttack();

            ShootFireball();
        }

        private void ShootFireball()
        {
            Debug.Log("Shoot Fireball");
        }
    }
}