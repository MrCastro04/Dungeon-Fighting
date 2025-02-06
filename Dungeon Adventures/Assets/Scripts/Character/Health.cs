using UnityEngine;

namespace Character
{
    public class Health : MonoBehaviour
    {
        private float _heathPoints;

        public float HeathPoints => _heathPoints;

        public void TakeDamage(float damageAmount)
        {
            _heathPoints = Mathf.Max(_heathPoints - damageAmount, 0f);

            if (_heathPoints == 0)
            {

            }
        }
    }
}