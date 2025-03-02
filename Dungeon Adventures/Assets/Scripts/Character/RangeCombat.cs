using System;
using Character.Mage;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(FireballPool))]
    public class RangeCombat : Combat
    {
        [SerializeField] private Transform _firePoint;

        [NonSerialized] public float RangeDamage = 10f;
        [NonSerialized] public float ProjectileSpeed = 10f;

        private GameObject _player;
        private FireballPool _fireballPool;

        protected override void Awake()
        {
            base.Awake();

            _fireballPool = GetComponent<FireballPool>();

            _player = _player == null ? GetPlayer() : _player;
        }

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

        public GameObject GetPlayer()
        {
            EnemyMageController enemyMageController = GetComponent<EnemyMageController>();

            GameObject player = enemyMageController.Player;

            return player;
        }

        private void ShootFireball()
        {
            if(_player == null) return;

            Fireball fireball = _fireballPool.GetFireball();

            fireball.transform.position = _firePoint.position;

            Vector3 direction = (_player.transform.position - transform.position).normalized;

            fireball.Instantiate( ProjectileSpeed , RangeDamage , direction , _fireballPool);
        }
    }
}