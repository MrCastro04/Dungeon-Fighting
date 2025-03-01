using Character.Mage;
using ScriptableObjects;
using Uitility;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Health))]

    public class EnemyController : MonoBehaviour
    {
        [SerializeField] protected CharacterStatsSO _enemyStats;

        protected AIBaseState _currentState;
        protected AIChaseState _chaseState = new();
        protected AIAttackState _attackState = new();
        protected AIDefeatState _defeatState = new();

        [field: SerializeField] public float AttackRange { get; private set; }

        public AIChaseState ChaseState => _chaseState;
        public AIAttackState AttackState => _attackState;
        public GameObject Player { get; protected set; }
        public Movement MovementCmp { get; protected set; }
        public Health HealthCmp { get; protected set; }
        public Combat CombatCmp { get; private set; }
        public Vector3 OriginalPosition { get; protected set; }
        public Vector3 OriginalRotation { get; protected set; }
        public float DistanceFromPlayer { get; protected set; }

        public virtual void Awake()
        {
            _currentState = _chaseState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.TAG_PLAYER);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            if (this is not EnemyMageController)
            {
                CombatCmp = CombatCmp == null ? GetComponent<Combat>() : CombatCmp;
            }
        }

        private void OnEnable()
        {
            HealthCmp.OnStartEnemyDefeated += HandleStartEnemyDefeated;
        }

        private void OnDisable()
        {
            HealthCmp.OnStartEnemyDefeated -= HandleStartEnemyDefeated;
        }

        public virtual void Start()
        {
            _currentState.EnterState(this);

            HealthCmp.HealthPoints = _enemyStats.healthPoints;

            CombatCmp.Damage = _enemyStats.damage;

            MovementCmp.NavMeshAgent.speed = _enemyStats.speed;

            HealthCmp.SliderCmp.maxValue = HealthCmp.HealthPoints;

            HealthCmp.SliderCmp.value = HealthCmp.HealthPoints;
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
