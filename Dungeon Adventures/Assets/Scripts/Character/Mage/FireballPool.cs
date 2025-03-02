using System.Collections.Generic;
using UnityEngine;

namespace Character.Mage
{
    public class FireballPool : MonoBehaviour
    {
        [SerializeField] private Fireball _fireballPrefab;
        [SerializeField, Min(3f)] private int _poolSize;

        private Queue <Fireball> _fireballPool = new Queue<Fireball>();

        private void Awake()
        {
            PopulatePool();
        }

        private void PopulatePool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                Fireball fireball = Instantiate(_fireballPrefab);

                fireball.gameObject.SetActive(false);

                _fireballPool.Enqueue(fireball);
            }
        }

        public Fireball GetFireball()
        {
            if (_fireballPool.Count > 0)
            {
                Fireball fireball = _fireballPool.Dequeue();

                fireball.gameObject.SetActive(true);

                return fireball;
            }

            Fireball newFireball = Instantiate(_fireballPrefab);

            return newFireball;
        }

        public void ReturnToPool(Fireball fireball)
        {
            fireball.gameObject.SetActive(false);

            _fireballPool.Enqueue(fireball);
        }
    }
}