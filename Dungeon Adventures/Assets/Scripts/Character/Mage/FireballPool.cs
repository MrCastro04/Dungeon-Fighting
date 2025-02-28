using System.Collections.Generic;
using UnityEngine;

namespace Character.Mage
{
    public class FireballPool : MonoBehaviour
    {
        public static FireballPool Instance;

        [SerializeField] private Fireball _fireballPrefab;
        [SerializeField] private int _poolSize = 5;

        private Queue <Fireball> _fireballPool = new Queue<Fireball>();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                PopulatePool();
            }
            else
            {
                Destroy(gameObject);
            }
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