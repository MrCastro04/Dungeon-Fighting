using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Movement))]

    public class EnemyController : MonoBehaviour
    {
        [field: SerializeField] public float ChaseRange { get; private set; }

        private AIBaseState _currentState;
        private AIReturnState _returnState = new();
        private AIChaseState _chaseState = new();

        public Movement MovementCmp { get; private set; }
        public Vector3 OriginalPosition { get; private set; }
        public Vector3 OriginalRotation { get; private set; }


        private void Awake()
        {
            _currentState = _returnState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            MovementCmp = GetComponent<Movement>();
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
