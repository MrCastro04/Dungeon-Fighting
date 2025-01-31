using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Movement))]

    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float _chaseRange;

        private AIBaseState _currentState;
        private AIReturnState _returnState = new();
        private AIChaseState _chaseState = new();

        public Movement Movement { get; private set; }
        public Vector3 OriginalPosition { get; private set; }
        public Quaternion OriginalRotation { get; private set; }

        private void Awake()
        {
            _currentState = _returnState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.rotation;

            Movement = GetComponent<Movement>();
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
