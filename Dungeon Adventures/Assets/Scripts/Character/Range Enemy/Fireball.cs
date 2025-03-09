using Character.FOR_ALL_CHARACTERS;
using Character.Player;
using Uitility;
using UnityEngine;

namespace Character.Range_Enemy
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(SphereCollider))]

    public class Fireball : MonoBehaviour
    {
        private AudioClip _hitSound;
        private FireballPool _fireballPool;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _flyingSpeed;
        private float _damage;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Instantiate (float flyingSpeed , float damage , Vector3 direction, FireballPool fireballPool , AudioClip hitSound)
        {
            _flyingSpeed = flyingSpeed;

            _damage = damage;

            _direction = direction;

            _fireballPool = fireballPool;

            _hitSound = hitSound;
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

                AudioSource.PlayClipAtPoint(_hitSound, transform.position);

                _fireballPool.ReturnToPool(this);
            }

            else if (other.CompareTag(Constants.TAG_OBSTACLE) || other.GetComponent<Fireball>())
            {
                AudioSource.PlayClipAtPoint(_hitSound, transform.position);

                _fireballPool.ReturnToPool(this);
            }
        }
    }
}
