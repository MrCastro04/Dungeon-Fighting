using Character_Stats;
using Uitility;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(BoxCollider))]

    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private CharacterStatsSO _enemyStats;

        private AIBaseState _currentState;
        private AIChaseState _chaseState = new();
        private AIAttackState _attackState = new();
        private AIDefeatState _defeatState = new();

        [field: SerializeField] public float AttackRange { get; private set; }

        public AIChaseState ChaseState => _chaseState;
        public AIAttackState AttackState => _attackState;
        public GameObject Player { get; private set; }
        public Movement MovementCmp { get; private set; }
        public Health HealthCmp { get; private set; }
        public Combat CombatCmp { get; private set; }
        public Vector3 OriginalPosition { get; private set; }
        public Vector3 OriginalRotation { get; private set; }
        public float DistanceFromPlayer { get; private set; }

        private void Awake()
        {
            _currentState = _chaseState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.PLAYER_TAG);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            CombatCmp = GetComponent<Combat>();
        }

        private void OnEnable()
        {
            HealthCmp.OnStartEnemyDefeated += HandleStartEnemyDefeated;
        }

        private void OnDisable()
        {
            HealthCmp.OnStartEnemyDefeated -= HandleStartEnemyDefeated;
        }

        private void Start()
        {
            HealthCmp.HeathPoints = _enemyStats.healthPoints;

            CombatCmp.Damage = _enemyStats.damage;

            MovementCmp.NavMeshAgent.speed = _enemyStats.speed;

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

        private void HandleStartEnemyDefeated()
        {
           SwitchState(_defeatState);

           _currentState.EnterState(this);
        }
    }
}
