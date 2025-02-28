using Uitility;
using UnityEngine;

namespace Character.Mage
{
    public class BossMage : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireRate = 2f;
        [SerializeField] private float _fireballSpeed = 5f;
        [SerializeField] private float _fireballDamage = 5f;

        private GameObject _player;
        private float _nextFireTime = 0f;

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER);
        }

        private void Update()
        {
            if (Time.time >= _nextFireTime)
            {
                ShootFireball();

                _nextFireTime = Time.time + _fireRate;
            }
        }

        private void ShootFireball()
        {
            Fireball fireball = FireballPool.Instance.GetFireball();

            fireball.transform.position = _firePoint.position;

            Vector3 fireballDirection = (_player.transform.position - transform.position).normalized;

            fireball.Instantiate( fireballDirection, _fireballSpeed , _fireballDamage );
        }
    }
}