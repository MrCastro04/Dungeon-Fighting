using Uitility;
using UnityEngine;

namespace Character.Mage
{
    [RequireComponent(typeof(Rigidbody))]
    public class Fireball : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _flyingSpeed;
        private float _damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Instantiate(Vector3 direction, float flyingSpeed , float damage)
        {
            _direction = direction;

            _flyingSpeed = flyingSpeed;

            _damage = damage;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction * _flyingSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Constants.TAG_PLAYER))
            {
                PlayerController player = other.GetComponent<PlayerController>();

                Health healthCmp = player.GetComponent<Health>();

                healthCmp.TakeDamage(_damage);

                FireballPool.Instance.ReturnToPool(this);
            }

            else
            {
                FireballPool.Instance.ReturnToPool(this);
            }
        }
    }
}
