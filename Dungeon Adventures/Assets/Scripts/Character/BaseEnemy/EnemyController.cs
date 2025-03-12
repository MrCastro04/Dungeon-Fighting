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
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CharacterSoundController))]

    public class EnemyController : MonoBehaviour , IHealthable , IControllerType
    {
        [SerializeField] protected CharacterStatsSO _enemyStats;

        protected AIBaseState _currentState;
        protected AIChaseState _chaseState = new();
        protected AIAttackState _attackState = new();

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
            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.TAG_PLAYER);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            _characterSoundControllerCmp = GetComponent<CharacterSoundController>();

            if (this is not EnemyMageController || this is not BossController)
            {
                CombatCmp = CombatCmp == null ? GetComponent<Combat>() : CombatCmp;
            }

            _currentState = _chaseState;
        }

        protected virtual void Start()
        {
            InitializeEnemyStats();

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

        public IControllerType GetSelfType()
        {
            return this;
        }

        private void CalculateDistanceFromPlayer()
        {
            if(Player == null) return;

            Vector3 enemyPosition = transform.position;

            Vector3 playerPosition = Player.transform.position;

            DistanceFromPlayer = Vector3.Distance(enemyPosition, playerPosition);
        }

        private void InitializeEnemyStats()
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
        }
    }
}
