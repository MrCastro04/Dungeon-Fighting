using System;
using UnityEngine;

namespace Character
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseRange;

        private AIBaseState _currentState;
        private AIChaseState _chaseState = new ();

        private void Awake()
        {
         
        }
    }
}
