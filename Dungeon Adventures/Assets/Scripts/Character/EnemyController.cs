using Uitility;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Movement))]

    public class EnemyController : MonoBehaviour
    {
        private AIBaseState _currentState;
        private AIReturnState _returnState = new();
        private AIChaseState _chaseState = new();

        [field: SerializeField] public float ChaseRange { get; private set; }

        public AIReturnState ReturnState => _returnState;
        public AIChaseState ChaseState => _chaseState;
        public GameObject Player { get; private set; }
        public Movement MovementCmp { get; private set; }
        public Vector3 OriginalPosition { get; private set; }
        public Vector3 OriginalRotation { get; private set; }
        public float DistanceFromPlayer { get; private set; }

        private void Awake()
        {
            _currentState = _returnState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.PLAYER_TAG);

            MovementCmp = GetComponent<Movement>();
        }

        private void Start()
        {
            _currentState.EnterState(this);
        }

        private void Update()
        {
            CalculateDistanceFromPlayer();

            _currentState.UpdateState(this);
        }

        public void SwitchState(AIBaseState newState)
        {
            _currentState = newState;

            _currentState.EnterState(this);
        }

        private void CalculateDistanceFromPlayer()
        {
            if(Player == null) return;

            Vector3 enemyPosition = transform.position;

            Vector3 playerPosition = Player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(transform.position, ChaseRange);
        }
    }
}
