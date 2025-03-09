using Character.Boss;
using Character.FOR_ALL_CHARACTERS;
using Character.Range_Enemy;
using Interfaces;
using ScriptableObjects;
using Uitility;
using UnityEngine;
using Utility;

namespace Character.BaseEnemy
{
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Health))]

    public class EnemyController : MonoBehaviour , IHealthable , IControllerType
    {
        [SerializeField] protected CharacterStatsSO _enemyStats;

        protected AIBaseState _currentState;
        protected AIChaseState _chaseState = new();
        protected AIAttackState _attackState = new();
        protected AIDefeatState _defeatState = new();

        private CharacterSoundController _characterSoundControllerCmp;

        public AIChaseState ChaseState => _chaseState;
        public AIAttackState AttackState => _attackState;
        public GameObject Player { get; protected set; }
        public Movement MovementCmp { get; protected set; }
        public Health HealthCmp { get; protected set; }
        public Combat CombatCmp { get; private set; }
        public Vector3 OriginalPosition { get; protected set; }
        public Vector3 OriginalRotation { get; protected set; }
        public float DistanceFromPlayer { get; protected set; }
        public float AttackRange { get; protected set; }

        protected virtual void Awake()
        {
            _characterSoundControllerCmp = GetComponent<CharacterSoundController>();

            _currentState = _chaseState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.TAG_PLAYER);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            if (this is not EnemyMageController || this is not BossController)
            {
                CombatCmp = CombatCmp == null ? GetComponent<Combat>() : CombatCmp;
            }
        }

        protected virtual void OnEnable()
        {
            HealthCmp.OnStartEnemyDefeated += HandleStartEnemyDefeated;
        }

        protected virtual void OnDisable()
        {
            HealthCmp.OnStartEnemyDefeated -= HandleStartEnemyDefeated;
        }

        protected virtual void Start()
        {
            HealthCmp.HealthPoints = _enemyStats.HealthPoints;

            HealthCmp.OriginHealthPoints = HealthCmp.HealthPoints;

            MovementCmp.NavMeshAgent.speed = _enemyStats.Speed;

            AttackRange = _enemyStats.AttackRange;

            HealthCmp.SliderCmp.maxValue = HealthCmp.HealthPoints;

            HealthCmp.SliderCmp.value = HealthCmp.HealthPoints;

            if (CombatCmp != null)
            {
                CombatCmp.Damage = _enemyStats.MeeleDamage;
            }

            _currentState.EnterState(this);
        }

        protected virtual void Update()
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

        public IControllerType GetSelfType()
        {
            return this;
        }
    }
}
