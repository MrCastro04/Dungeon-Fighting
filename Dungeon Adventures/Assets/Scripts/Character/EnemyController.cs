using System;
using UnityEngine;

namespace Character
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseRange;

        private AIBaseState _currentState;
        private AIReturnState _returnState = new();
        private AIChaseState _chaseState = new ();

        private void Awake()
        {
            _currentState = _returnState;
        }

        private void Start()
        {
            _currentState.EnterState(this);
        }

        private void Update()
        {
            _currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            _currentState = newState;

            _currentState.EnterState(this);
        }
    }
}
