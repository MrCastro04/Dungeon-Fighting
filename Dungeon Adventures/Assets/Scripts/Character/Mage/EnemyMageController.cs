using ScriptableObjects;
using Uitility;
using UnityEngine;

namespace Character.Mage
{
    [RequireComponent(typeof(RangeCombat))]
    public class EnemyMageController : EnemyController
    {
        [SerializeField] private RangeCharacterStatsSO _rangeCharacterStats;

        private RangeCombat _rangeCombatCmp;

        public override void Awake()
        {
            _currentState = _chaseState;

            OriginalPosition = transform.position;

            OriginalRotation = transform.forward;

            Player = GameObject.FindWithTag(Constants.TAG_PLAYER);

            MovementCmp = GetComponent<Movement>();

            HealthCmp = GetComponent<Health>();

            _rangeCombatCmp = GetComponent<RangeCombat>();
        }

        public override void Start()
        {
            _enemyStats = _rangeCharacterStats;

            HealthCmp.HealthPoints = _enemyStats.healthPoints;

            _rangeCombatCmp.RangeDamage = _rangeCharacterStats.ProjectileDamage;

            
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
