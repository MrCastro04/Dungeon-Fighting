using System;
using Uitility;
using UnityEngine;

namespace Character
{
    public class Health : MonoBehaviour
    {
        private Animator _animatorCmp;
        private float _heathPoints;

        public float HeathPoints { get; set; }

        private void Awake()
        {
            _animatorCmp = GetComponentInChildren<Animator>();
        }

        public void TakeDamage(float damageAmount)
        {
            _heathPoints = Mathf.Max(_heathPoints - damageAmount, 0f);

            if (_heathPoints == 0)
            {
               PlayDefeatAnimation();
            }
        }

        private void PlayDefeatAnimation()
        {
            _animatorCmp.SetTrigger(Constants.DEFEAT_ANIMATOR_PARAM);
        }
    }
}